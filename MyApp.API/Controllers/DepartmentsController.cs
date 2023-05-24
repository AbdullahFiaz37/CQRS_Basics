using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.API.DTOs;
using MyApp.CQRS.Commands.Interfaces;
using MyApp.CQRS.Data.Models;
using MyApp.CQRS.Models.Request;
using MyApp.CQRS.Models.Response;
using MyApp.CQRS.Queries.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp.API.Controllers
{
    /// <summary>
    /// Controller for managing departments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentCommand _departmentCommand;
        private readonly IDepartmentQuery _departmentQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentsController"/> class.
        /// </summary>
        /// <param name="departmentCommand">The department command service.</param>
        /// <param name="departmentQuery">The department query service.</param>
        public DepartmentsController(IDepartmentCommand departmentCommand, IDepartmentQuery departmentQuery)
        {
            _departmentCommand = departmentCommand;
            _departmentQuery = departmentQuery;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _departmentQuery.GetAllDepartments();
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var response = await _departmentQuery.GetDepartmentById(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="createDepartmentDTO">The DTO containing the department data.</param>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDTO createDepartmentDTO)
        {
            try
            {
                DepartmentModel department = new DepartmentModel
                {
                    DepartmentId = createDepartmentDTO.DepartmentId,
                    DepartmentName = createDepartmentDTO.DepartmentName
                };

                var response = await _departmentCommand.CreateDepartment(department);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updateDepartmentDTO">The DTO containing the updated department data.</param>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDepartmentDTO updateDepartmentDTO)
        {
            try
            {
                DepartmentModel department = new DepartmentModel
                {
                    DepartmentId = updateDepartmentDTO.DepartmentId,
                    DepartmentName = updateDepartmentDTO.DepartmentName
                };

                var response = await _departmentCommand.UpdateDepartment(department);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var response = await _departmentCommand.DeleteDepartment(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse(false, 500, ex.Message, null, null));
            }
        }
    }
}
