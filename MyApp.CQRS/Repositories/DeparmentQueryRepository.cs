using Microsoft.EntityFrameworkCore;
using MyApp.CQRS.Data;
using MyApp.CQRS.Models;
using MyApp.CQRS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.CQRS.Repositories
{
    /// <summary>
    /// Repository for querying departments.
    /// </summary>
    public class DepartmentQueryRepository : IDepartmentQueryRepository
    {
        private readonly MyAppDbContext _myAppDbContext;

        /// <summary>
        /// Initializes a new instance of the DepartmentQueryRepository class.
        /// </summary>
        /// <param name="myAppDbContext">The application's database context.</param>
        public DepartmentQueryRepository(MyAppDbContext myAppDbContext)
        {
            _myAppDbContext = myAppDbContext;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns the list of departments.</returns>
        public async Task<List<Department>> GetAllAsync()
        {
            try
            {
                return await _myAppDbContext.tblDepartments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving the departments.", ex);
            }
        }

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the department.</returns>
        public async Task<Department> GetByIdAsync(int id)
        {
            try
            {
                return await _myAppDbContext.tblDepartments.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving the department.", ex);
            }
        }
    }
}
