using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.Model
{
    public class ApplicationUser : IdentityUser
    {
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

        [Column(TypeName = "DateTime2")]
        public DateTime RegistrationDate { get; set; }
    }
}