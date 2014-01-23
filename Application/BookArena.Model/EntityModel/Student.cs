using System.ComponentModel.DataAnnotations;

namespace BookArena.Model.EntityModel
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string IdCardNumber { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string Batch { get; set; }
    }
}