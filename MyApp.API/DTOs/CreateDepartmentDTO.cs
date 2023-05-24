using System.ComponentModel.DataAnnotations;

namespace MyApp.API.DTOs
{
    public class CreateDepartmentDTO
    {
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
