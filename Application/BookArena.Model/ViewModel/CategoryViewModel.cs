using System.Collections.Generic;

namespace BookArena.Model.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}