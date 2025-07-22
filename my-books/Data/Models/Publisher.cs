namespace my_books.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Navigations Properties
        public List<Book> Books { get; set; }
    }
}
