using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using NUnit.Framework;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BooksDbTest")
            .Options;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
        AppDbContext context;
        PublishersService publishersService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            publishersService = new PublishersService(context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers(null, null, null);

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.AreEqual(result.Count, 6);
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers(null, null, 2);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers(null, "3", null);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("name_desc", null, null);

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
        }

        [Test, Order(5)]
        public void GetPublisherById_WithoutResponse_Test()
        {
            var result = publishersService.GetPublisherById(77);

            Assert.That(result, Is.Null);
        }

        [Test, Order(6)]
        public void GetPublisherById_WithResponse_Test()
        {
            var result = publishersService.GetPublisherById(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.Name, Is.EqualTo("Publisher 2"));
        }

        [Test, Order(9)]
        public void GetPublisherData_Test()
        {
            var result = publishersService.GetPublisherWithBooksAndAuthorsById(1);

            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.BookAuthors, Is.Not.Empty);
            Assert.That(result.BookAuthors.Count, Is.GreaterThan(0));

            var firstBookName = result.BookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("1st Book Title"));
        }

        [Test, Order(9)]
        public void DeletePublisherById_Test()
        {
            publishersService.DeletePublisherById(4);

            var result = publishersService.GetPublisherById(4);

            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        public void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                },
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);

            var books = new List<Book>
            {
                new Book()
                {
                    Title = "1st Book Title",
                    Description = "1st Book Description",
                    IsRead = true,
                    DateRead = DateTime.Now,
                    Rate = 4,
                    Genre = "Biography",
                    CoverUrl = "https.....",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1,
                },
                new Book()
                {
                    Title = "2nd Book Title",
                    Description = "2nd Book Description",
                    IsRead = false,
                    Genre = "Biography",
                    CoverUrl = "https.....",
                    DateAdded = DateTime.Now.AddDays(-7),
                    PublisherId = 1,
                }
            };
            context.Books.AddRange(books);

            var book_authors = new List<Book_Author>
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1,
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2,
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2,
                }
            };
            context.Book_Authors.AddRange(book_authors);

            context.SaveChanges();
        }

    }
}