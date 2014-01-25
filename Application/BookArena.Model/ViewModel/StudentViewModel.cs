using System.Collections.Generic;
using BookArena.Model.EntityModel;

namespace BookArena.Model.ViewModel
{
    public class StudentViewModel : Student
    {
        public string FullName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}