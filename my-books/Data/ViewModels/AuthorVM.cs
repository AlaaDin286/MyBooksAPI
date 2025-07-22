namespace my_books.Data.ViewModels
{
    public class AuthorVM
    {
        public required string FullName { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public required string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
