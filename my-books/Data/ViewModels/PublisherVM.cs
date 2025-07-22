namespace my_books.Data.ViewModels
{
    public class PublisherVM
    {
        public required string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public required string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }

    public class BookAuthorVM 
    {
        public required string BookName { get; set;}
        public List<string> Authors { get; set; }
    }
}
