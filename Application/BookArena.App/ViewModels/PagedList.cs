using System.Collections.Generic;

namespace BookArena.App.ViewModels
{
    public class PagedList<T>
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T> Entities { get; set; }
    }
}