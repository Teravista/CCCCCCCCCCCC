using System.ComponentModel.DataAnnotations;

namespace Webber.Dtos
{
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1,200)]
        public decimal Price {get;init;}
    }
} 