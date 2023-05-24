using MyApp.CQRS.Models;
using MyApp.CQRS.Models.Request;
using MyApp.CQRS.Models.Response;
using System.Threading.Tasks;

namespace MyApp.CQRS.Commands.Interfaces
{
    /// <summary>
    /// Interface for department commands.
    /// </summary>
    public interface IDepartmentCommand
    {
        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department model containing the department data.</param>
        /// <returns>A task representing the asynchronous operation with a JSON response.</returns>
        Task<int> CreateDepartment(Department department);

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="department">The department model containing the updated department data.</param>
        /// <returns>A task representing the asynchronous operation with a JSON response.</returns>
        Task<int> UpdateDepartment(Department department);

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>A task representing the asynchronous operation with a JSON response.</returns>
        Task<int> DeleteDepartment(int id);
    }
}
