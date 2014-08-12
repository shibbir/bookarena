using System.Collections.Generic;

namespace BookArena.App.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}