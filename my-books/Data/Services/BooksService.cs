using my_books.Data.Models;
using my_books.Data.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext dbContext;

        public BooksService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddBook(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead : null,
                Rate = bookVM.IsRead ? bookVM.Rate : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            };

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            foreach(var id in bookVM.AuthorsIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = book.Id,
                    AuthorId = id
                };
                dbContext.Book_Authors.Add(book_author);
                dbContext.SaveChanges();
            }

        }
        public List<Book> GetAllBooks() => dbContext.Books.ToList();
        //public Book? GetBookById(int id) => dbContext.Books.Find(id);
        //public Book GetBookById(int id) => dbContext.Books.FirstOrDefault(x => x.Id == id);
        public BookWithAuthorsVM? GetBookById(int id)
        {
            var bookWithAuthorsVM = dbContext.Books.Where(n => n.Id == id).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorsNames = book.Book_Authors.Select(ba => ba.Author.FullName).ToList()
            }).FirstOrDefault();

            return bookWithAuthorsVM;
        }

        public Book? UpdateBookById(int id, BookVM bookVM)
        {
            var book = dbContext.Books.Find(id);

            if (book is not null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.IsRead ? bookVM.DateRead : null;
                book.Rate = bookVM.IsRead ? bookVM.Rate : null;
                book.Genre = bookVM.Genre;
                book.CoverUrl = bookVM.CoverUrl;

                dbContext.SaveChanges();
            }

            return book;
        }

        public bool DeleteBookById(int id)
        {
            var book = dbContext.Books.Find(id);
            if (book is not null)
            {
                dbContext.Books.Remove(book);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
