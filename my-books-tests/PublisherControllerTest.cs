using Microsoft.EntityFrameworkCore;
using my_books.Data.Services;
using my_books.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using my_books.Controllers;
using Microsoft.Extensions.Logging;
using my_books.Data.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.ViewModels;

namespace my_books_tests
{
    public class PublisherControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BooksDbControllerTest")
            .Options;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
        AppDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;
        //ILogger<PublishersController> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_WithNoSortBySearchPageNb_ReturnOK_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publisher", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
            Assert.That(actionResultData.FirstOrDefault().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count, Is.EqualTo(5));

            IActionResult actionResult_2 = publishersController.GetAllPublishers("name_desc", "Publisher", 2);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            actionResultData = (actionResult_2 as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.FirstOrDefault().Name, Is.EqualTo("Publisher 1"));
            Assert.That(actionResultData.FirstOrDefault().Id, Is.EqualTo(1));
            Assert.That(actionResultData.Count, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisher_ById_ReturnsOK_Test()
        {
            IActionResult actionResult = publishersController.GetPublisherById(4);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(actionResultData.Name, Is.EqualTo("publisher 4").IgnoreCase);
            Assert.That(actionResultData.Id, Is.EqualTo(4));
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisher_ById_ReturnsNotFound_Test()
        {
            IActionResult actionResult = publishersController.GetPublisherById(44);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = "New Publisher"
            };
            IActionResult actionResult = publishersController.AddPublisher(publisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequest_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = null
            };
            IActionResult actionResult = publishersController.AddPublisher(publisherVM);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_ReturnsOk_Test()
        {
            IActionResult actionResult = publishersController.DeletePublisherById(6);

            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherById_ReturnsBadRequest_Test()
        {
            IActionResult actionResult = publishersController.DeletePublisherById(66);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
            
            var message = (actionResult as BadRequestObjectResult).Value as string;

            Assert.That(message, Is.EqualTo("The publisher with id:66 does not exists."));
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

            context.SaveChanges();
        }
    }
}
