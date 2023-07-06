using System.ComponentModel.DataAnnotations;

namespace Project1.Dtos
{
    public class CreateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
