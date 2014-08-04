using System.Collections.Generic;

namespace BookArena.Core
{
    public class PagedResult<T>
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T> Entities { get; set; }
        public int TotalCount { get; set; }
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}