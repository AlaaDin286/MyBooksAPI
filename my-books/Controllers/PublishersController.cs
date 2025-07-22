using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService publishersService;
        private readonly ILogger<PublishersController> logger;
        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            this.publishersService = publishersService;
            this.logger = logger;
        }

        [HttpPost("add-publisher")]
        [Authorize(Roles = "Writer")]
        public IActionResult AddPublisher(PublisherVM publisher)
        {
            try
            {
                var newPublisher =  publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher),newPublisher);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{id:int}")]
        [Authorize]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = publishersService.GetPublisherById(id);
            if (publisher != null) 
            {
                return Ok(publisher);
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-with-books-and-authors-by-id/{id:int}")]
        [Authorize]
        public IActionResult GetPublisherWithBooksAndAuthorsById(int id)
        {
            var publisherWithBooksAndAuthors = publishersService.GetPublisherWithBooksAndAuthorsById(id);
            return Ok(publisherWithBooksAndAuthors);
        }

        [HttpGet("get-all-publishers")]
        [Authorize]
        public IActionResult GetAllPublishers(string? SortBy, string? searchString, int? pageNumber)
        {
            try
            {
                logger.LogInformation("This is just a log in GetAllPublishers()");
                var allPublishers = publishersService.GetAllPublishers(SortBy, searchString, pageNumber);
                return Ok(allPublishers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("delete-publisher-by-id/{id:int}")]
        [Authorize(Roles = "Writer")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                publishersService.DeletePublisherById(id);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
