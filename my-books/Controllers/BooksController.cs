using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService booksService;

        public BooksController(BooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = booksService.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id:int}")]
        // or [Route("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            var book = booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            booksService.AddBook(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id:int}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updatedBook = booksService.UpdateBookById(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book/{id:int}")]
        public IActionResult DeleteBookById(int id)
        {
            bool isDeleted = booksService.DeleteBookById(id);
            if (isDeleted is false) return NotFound(id);
            return Ok();
        }
    }
}
