using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookArena.Model
{
    public class Category
    {
        //public Category()
        //{
        //    Books = new HashSet<Book>();
        //}
        public int CategoryId { get; set; }

        //[Required]
        //[StringLength(16, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        public int i { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}