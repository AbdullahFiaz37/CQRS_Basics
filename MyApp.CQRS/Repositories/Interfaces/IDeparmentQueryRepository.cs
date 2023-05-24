using MyApp.CQRS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.CQRS.Repositories.Interfaces
{
    /// <summary>
    /// Represents the interface for the Department query repository.
    /// </summary>
    public interface IDepartmentQueryRepository
    {
        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns the list of departments.</returns>
        Task<List<Department>> GetAllAsync();

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the department.</returns>
        Task<Department> GetByIdAsync(int id);
    }
}