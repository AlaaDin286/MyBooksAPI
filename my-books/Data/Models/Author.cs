namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public required string FullName { get; set; }

        // Navigations Properties
        public List<Book_Author> Book_Authors { get; set; }
    }
}
