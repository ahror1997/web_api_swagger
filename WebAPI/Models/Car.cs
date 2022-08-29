using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Year { get; set; }
    }
}
