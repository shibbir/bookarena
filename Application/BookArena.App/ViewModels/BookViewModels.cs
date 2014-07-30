namespace BookArena.App.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageFileName { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class BasicBookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ImageFileName { get; set; }
        public int AvailableQuantity { get; set; }
    }
}