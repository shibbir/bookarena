using System.ComponentModel.DataAnnotations;

namespace BookArena.Model.EntityModel
{
    public class Config
    {
        [Key]
        public int Id { get; set; }

        public decimal FineAmount { get; set; }

        public int BookRentDurationInDays { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Institute { get; set; }
    }
}