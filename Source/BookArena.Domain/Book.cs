﻿using System.ComponentModel.DataAnnotations;

namespace BookArena.Model
{
    public class Book
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Author { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string ShortDescription { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LongDescription { get; set; }

        public string ImageFileName { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public virtual Category Category { get; set; }
    }
}