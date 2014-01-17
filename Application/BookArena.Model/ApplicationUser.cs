using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model
{
    [Table("ApplicationUser")]
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Contact { get; set; }

        [Required]
        [EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Address { get; set; }
    }
}