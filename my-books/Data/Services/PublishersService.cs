using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext dbContext;

        public PublishersService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };
            dbContext.Publishers.Add(publisher);
            dbContext.SaveChanges();

            return publisher;
        }

        public Publisher? GetPublisherById(int id)
        {
            var publisher = dbContext.Publishers.FirstOrDefault(x => x.Id == id);
            return publisher;
        }

        public PublisherWithBooksAndAuthorsVM? GetPublisherWithBooksAndAuthorsById(int id)
        {
            var publisherWithBooksAndAuthorsVM = dbContext.Publishers.Where(p => p.Id == id).
                Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b => new BookAuthorVM()
                    {
                        BookName = b.Title,
                        Authors = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return publisherWithBooksAndAuthorsVM;
        }

        public List<Publisher> GetAllPublishers(string? SortBy, string? searchString, int? pageNumber)
        {
            var allPublishers = dbContext.Publishers.OrderBy(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(SortBy))
            {
                switch (SortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(p => p.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(p => p.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            //Paging
            if (pageNumber is not null) 
            {
                int pageSize = 5;
                allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);
            }
            return allPublishers;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher is not null)
            {
                dbContext.Publishers.Remove(publisher);
                dbContext.SaveChanges();
            } else
            {
                throw new Exception($"The publisher with id:{id} does not exists.");
            }
        }
    }
}
