using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookArena.Model.EntityModel
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(16, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}