using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookArena.Model.EntityModel
{
    public class Transaction
    {
        public Transaction()
        {
            BorrowedDate = DateTime.UtcNow;
            ExpectedReturnDate = BorrowedDate.AddDays(9);
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime BorrowedDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ExpectedReturnDate { get; set; }
    }
}