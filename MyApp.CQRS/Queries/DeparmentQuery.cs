using Microsoft.EntityFrameworkCore;
using MyApp.CQRS.Data;
using MyApp.CQRS.Models.Response;
using MyApp.CQRS.Queries.Interfaces;
using MyApp.CQRS.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp.CQRS.Queries
{
    /// <summary>
    /// Represents the implementation of department queries.
    /// </summary>
    public class DepartmentQuery : IDepartmentQuery
    {
        private readonly IDepartmentQueryRepository _departmentQueryRepository;

        /// <summary>
        /// Initializes a new instance of the DepartmentQuery class.
        /// </summary>
        /// <param name="departmentQueryRepository">The repository for department queries.</param>
        public DepartmentQuery(IDepartmentQueryRepository departmentQueryRepository)
        {
            _departmentQueryRepository = departmentQueryRepository;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a JSON response containing the list of departments.</returns>
        public async Task<JsonResponse> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentQueryRepository.GetAllAsync();
                return new JsonResponse(true, 200, "", departments, null);
            }
            catch (Exception ex)
            {
                // Returns a JsonResponse with an error message if an exception is thrown
                return new JsonResponse(false, 500, ex.Message, null, null);
            }
        }

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns a JSON response containing the department.</returns>
        public async Task<JsonResponse> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentQueryRepository.GetByIdAsync(id);
                if(department== null)
                {
                    return new JsonResponse(true, 404, "Department does not exist", null, null);
                }
                return new JsonResponse(true, 200, "", department, null);
            }
            catch (Exception ex)
            {
                // Returns a JsonRessponse with an error message if an exception is thrown
                return new JsonResponse(false, 500, ex.Message, null, null);
            }
        }
    }
}
