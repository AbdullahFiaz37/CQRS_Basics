using MyApp.CQRS.Data.Models;
using MyApp.CQRS.Models.Response;
using System.Threading.Tasks;

namespace MyApp.CQRS.Queries.Interfaces
{
    /// <summary>
    /// Represents the interface for department queries.
    /// </summary>
    public interface IDepartmentQuery
    {
        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a JSON response containing the list of departments.</returns>
        Task<JsonResponse> GetAllDepartments();

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns a JSON response containing the department.</returns>
        Task<JsonResponse> GetDepartmentById(int id);
    }
}
