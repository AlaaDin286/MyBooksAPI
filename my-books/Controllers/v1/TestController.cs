﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data1")]
        public IActionResult GetV1()
        {
            return Ok("This is TestController V1.0");
        }

        [HttpGet("get-test-data12"), MapToApiVersion("1.2")]
        public IActionResult GetV12()
        {
            return Ok("This is TestController V1.2");
        }

        [HttpGet("get-test-data19"), MapToApiVersion("1.9")]
        public IActionResult GetV19()
        {
            return Ok("This is TestController V1.9");
        }
    }
}
