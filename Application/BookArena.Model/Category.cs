using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}