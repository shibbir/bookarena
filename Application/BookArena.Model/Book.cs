using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string ShortDescription { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LongDescription { get; set; }

        public string ImageFileName { get; set; }
        public double Rating { get; set; }
        public int StatusId { get; set; }

        [NotMapped]
        public string CategoryTitle { get; set; }
    }
}