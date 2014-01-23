using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.Model.EntityModel
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Address { get; set; }

        [Url]
        [StringLength(25, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Website { get; set; }

        public string ImageFileName { get; set; }
    }
}