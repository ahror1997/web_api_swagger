using System.ComponentModel.DataAnnotations;

namespace WebAPI.Records
{
    public record CarRecord
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Year { get; set; }
    }
}