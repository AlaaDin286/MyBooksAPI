using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            this.authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id:int}")]
        public IActionResult GetAuthorWithBooksById(int id)
        {
            var authorWithBooks = authorsService.GetAuthorWithBooksById(id);
            return Ok(authorWithBooks);
        }
    }
}
