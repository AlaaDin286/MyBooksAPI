using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext dbContext;

        public AuthorsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };
            dbContext.Authors.Add(author);
            dbContext.SaveChanges();
        }

        public AuthorWithBooksVM? GetAuthorWithBooksById(int id)
        {
            var authorWithBooksVM = dbContext.Authors.Where(a => a.Id == id).Select(author => new AuthorWithBooksVM()
            {
                FullName = author.FullName,
                BookTitles = author.Book_Authors.Select(ba => ba.Book.Title).ToList()
            }).FirstOrDefault();

            return authorWithBooksVM;
        }
    }
}
