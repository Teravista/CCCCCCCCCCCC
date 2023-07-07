using System.ComponentModel.DataAnnotations;

namespace Project3.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Category { get; set; }
    }
}
