namespace BookArena.Model.EntityModels
{
    public class BookMetaData
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UniqueKey { get; set; }
        public bool IsAvailable { get; set; }
    }
}