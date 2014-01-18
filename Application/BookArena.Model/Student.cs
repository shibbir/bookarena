using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        [Required]
        public string IdCardNumber { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string Batch { get; set; }
    }
}