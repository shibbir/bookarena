namespace BookArena.Model.ViewModel
{
    public class BasicBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string ImageFileName { get; set; }
        public int AvailableQuantity { get; set; }
    }
}