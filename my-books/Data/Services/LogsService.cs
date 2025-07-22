using my_books.Data.Models;

namespace my_books.Data.Services
{
    public class LogsService
    {
        private readonly AppDbContext dbContext;

        public LogsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Log> GetAllLogsFromDb() => dbContext.Logs.ToList();
    }
}
