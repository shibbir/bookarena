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
            ReturnedDate = DateTime.UtcNow;

            FormattedBorrowedDate = BorrowedDate.ToShortDateString();
            FormattedExpectedReturnDate = ExpectedReturnDate.ToShortDateString();
            FormattedReturnedDate = ReturnedDate.ToShortDateString();

            Status = (IsActive ? "Cleared" : "Not Cleared");
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime BorrowedDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ExpectedReturnDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ReturnedDate { get; set; }

        [NotMapped]
        public string FormattedBorrowedDate { get; set; }

        [NotMapped]
        public string FormattedExpectedReturnDate { get; set; }

        [NotMapped]
        public string FormattedReturnedDate { get; set; }

        [NotMapped]
        public string Status { get; set; }
    }
}