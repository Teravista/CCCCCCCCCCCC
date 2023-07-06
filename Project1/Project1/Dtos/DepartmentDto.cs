using System.ComponentModel.DataAnnotations;

namespace Project1.Dtos
{
    public record DepartmentDto
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }
    }
}
