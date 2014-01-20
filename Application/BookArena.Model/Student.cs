using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string IdCardNumber { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string Batch { get; set; }
    }
}