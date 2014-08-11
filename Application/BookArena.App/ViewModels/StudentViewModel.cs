using System.Collections.Generic;
using BookArena.Model;

namespace BookArena.App.ViewModels
{
    public class StudentViewModel : Student
    {
        public ICollection<TransactionViewModel> Transactions { get; set; }
    }
}