using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApp.API.Datatypes.Enums;
using MyApp.API.DTOs;
using MyApp.CQRS.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.API.Controllers
{
    /// <summary>
    /// Controller for security-related operations such as user registration and login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SymmetricSecurityKey _key;

        public SecurityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["MyAppJWT:Secret"]));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerRequest)
        {
            try
            {
                // Find User Role In Database
                var role = await _roleManager.FindByNameAsync(RoleTypes.User.ToString());

                // Check if the role exists in the database
                if (role == null)
                {
                    return StatusCode(404, new JsonResponse(false, 404, "Role Does Not Exist", null, null));
                }
                else
                {
                    // Map the user request model with the User Identity model
                    var user = new IdentityUser
                    {
                        UserName = registerRequest.UserName,
                        Email = registerRequest.Email,
                        NormalizedEmail = registerRequest.Email,
                        NormalizedUserName = registerRequest.Email,
                        TwoFactorEnabled = false,
                        PhoneNumber = registerRequest.PhoneNo,
                        EmailConfirmed = false
                    };

                    // Insert the user record in the database
                    var result = await _userManager.CreateAsync(user, registerRequest.Password);

                    // Check if the user record was inserted successfully
                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(e => e.Description).ToList();
                        return StatusCode(403, new JsonResponse(false, 403, "Registration Failed", null, errors));
                    }

                    // Assign the role to the user
                    await _userManager.AddToRoleAsync(user, role.Name);

                    return StatusCode(200, new JsonResponse(true, 200, "User Registered Successfully", null, null));
                }
            }
            catch (Exception ex)
            {
                // Return an error response with a 500 status code if an exception occurs
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Performs user login.
        /// </summary>
        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] string usernameOrEmail, [FromQuery][DataType(DataType.Password)] string password)
        {
            try
            {
                // Find the user in the database by username or email
                var user = await _userManager.FindByEmailAsync(usernameOrEmail);

                if (user == null)
                {
                    // Find the user in the database by username
                    user = await _userManager.FindByNameAsync(usernameOrEmail);
                    if (user == null)
                    {
                        return StatusCode(403, new JsonResponse(false, 403, "Invalid Username or Email", null, null));
                    }
                }

                // Check the password in the database
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!result.Succeeded)
                {
                    return StatusCode(403, new JsonResponse(false, 403, "Invalid Password", null, null));
                }
                else
                {
                    string token = "";
                    string userRole = "";
                    var role = await _userManager.GetRolesAsync(user); // Retrieve the user roles
                    if (role.Count() > 0)
                    {
                        userRole = role.FirstOrDefault().ToLower();
                        token = GenerateToken(user.Id, userRole); // Assuming the user has only one role, you can use FirstOrDefault()
                    }
                    return StatusCode(200, new JsonResponse(true, 200, "Login Success", new { token = token, role = userRole }, null));
                }
            }
            catch (Exception ex)
            {
                // Return an error response with a 500 status code if an exception occurs
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Generates a JWT token for authentication.
        /// </summary>
        private string GenerateToken(string userId, string role)
        {
            try
            {
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,role),
                    new Claim("UserId",userId),
                    new Claim("UserRole",role)
                };

                var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = credentials,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
