using System.Collections.Generic;
using BookArena.Model.EntityModel;

namespace BookArena.Model.ViewModel
{
    public class SingleStudentDetailsViewModel : Student
    {
        public string FullName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}