using Microsoft.EntityFrameworkCore;
using MyApp.CQRS.Data;
using MyApp.CQRS.Models;
using MyApp.CQRS.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp.CQRS.Repositories
{
    /// <summary>
    /// Repository for performing create, update, and delete operations on the Department entity.
    /// </summary>
    public class DepartmentCommandRepository : IDepartmentCommandRepository
    {
        private readonly MyAppDbContext _myAppDbContext;

        /// <summary>
        /// Initializes a new instance of the DepartmentCommandRepository class.
        /// </summary>
        /// <param name="myAppDbContext">The application's database context.</param>
        public DepartmentCommandRepository(MyAppDbContext myAppDbContext)
        {
            _myAppDbContext = myAppDbContext;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department object to create.</param>
        /// <returns>The number of entities added.</returns>
        public async Task<int> CreateAsync(Department department)
        {
            _myAppDbContext.tblDepartments.Add(department);
            return _myAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="department">The department object to update.</param>
        /// <returns>The number of entities updated.</returns>
        public async Task<int> UpdateAsync(Department department)
        {
            _myAppDbContext.tblDepartments.Update(department);
            return _myAppDbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a department.
        /// </summary>
        /// <param name="department">The department object to delete.</param>
        /// <returns>The number of entities deleted.</returns>
        public async Task<int> DeleteAsync(Department department)
        {
            _myAppDbContext.tblDepartments.Remove(department);
            return _myAppDbContext.SaveChanges();
        }
    }
}
