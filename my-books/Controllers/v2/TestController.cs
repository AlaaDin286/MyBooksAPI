﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data2")]
        public IActionResult Get()
        {
            return Ok("This is TestController V2");
        }
    }
}
