using MyApp.CQRS.Commands.Interfaces;
using MyApp.CQRS.Models;
using MyApp.CQRS.Models.Request;
using MyApp.CQRS.Models.Response;
using MyApp.CQRS.Repositories;
using MyApp.CQRS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.CQRS.Commands
{
    // DepartmentCommand handles commands related to departments
    public class DepartmentCommand : IDepartmentCommand
    {
        private readonly IDepartmentCommandRepository _departmentCommandRepository;
        private readonly IDepartmentQueryRepository _departmentQueryRepository;

        /// <summary>
        /// Initializes a new instance of the DepartmentCommand class.
        /// </summary>
        /// <param name="departmentCommandRepository">The repository for department commands.</param>
        /// <param name="departmentQueryRepository">The repository for department queries.</param>
        public DepartmentCommand(
            IDepartmentCommandRepository departmentCommandRepository,
            IDepartmentQueryRepository departmentQueryRepository)
        {
            _departmentCommandRepository = departmentCommandRepository;
            _departmentQueryRepository = departmentQueryRepository;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="departmentRequest">The department details to create.</param>
        /// <returns>The JSON response indicating the result of the operation.</returns>
        public async Task<int> CreateDepartment(DepartmentM departmentRequest)
        {
            try
            {
                // Check if the department already exists
                var departments = await _departmentQueryRepository.GetAllAsync();
                if (departments.Any(d => d.DepartmentName == departmentRequest.DepartmentName))
                {
                    return new JsonResponse(true, 409, $"Department '{departmentRequest.DepartmentName}' already exists", null, null);
                }

                // Create a new department entity
                Department departmentEntity = new Department
                {
                    DepartmentId = departmentRequest.DepartmentId,
                    DepartmentName = departmentRequest.DepartmentName
                };

                var response = await _departmentCommandRepository.CreateAsync(departmentEntity);
                return new JsonResponse(true, 200, "Department created successfully", response, null);
            }
            catch (Exception ex)
            {
                return new JsonResponse(true, 500, ex.Message, null, null);
            }
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="departmentRequest">The department details to update.</param>
        /// <returns>The JSON response indicating the result of the operation.</returns>
        public async Task<int> UpdateDepartment(DepartmentModel departmentRequest)
        {
            try
            {
                // Check if the department exists
                var department = await _departmentQueryRepository.GetByIdAsync(departmentRequest.DepartmentId);
                if (department == null)
                {
                    return new JsonResponse(false, 404, "Department does not exist", null, null);
                }
                else
                {
                    // Check if the new department name already exists (excluding the current department)
                    var departments = await _departmentQueryRepository.GetAllAsync();
                    if (departments.Any(d => d.DepartmentName == departmentRequest.DepartmentName && d.DepartmentId != departmentRequest.DepartmentId))
                    {
                        return new JsonResponse(true, 409, $"Department '{departmentRequest.DepartmentName}' already exists", null, null);
                    }

                    department.DepartmentName = departmentRequest.DepartmentName;
                    var response = await _departmentCommandRepository.UpdateAsync(department);
                    return new JsonResponse(true, 200, "Department updated successfully", response, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>The JSON response indicating the result of the operation.</returns>
        public async Task<int> DeleteDepartment(int id)
        {
            try
            {
                // Check if the department exists
                var department = await _departmentQueryRepository.GetByIdAsync(id);
                if (department == null)
                {
                    return 0;
                }
                else
                {
                    var response = await _departmentCommandRepository.DeleteAsync(department);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
