using MyApp.CQRS.Models;
using System.Threading.Tasks;

namespace MyApp.CQRS.Repositories.Interfaces
{
    /// <summary>
    /// Represents the interface for the Department command repository.
    /// </summary>
    public interface IDepartmentCommandRepository
    {
        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department object to create.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> CreateAsync(Department department);

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="department">The department object to update.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> UpdateAsync(Department department);

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="departmentId">The ID of the department to delete.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> DeleteAsync(Department department);
    }
}
