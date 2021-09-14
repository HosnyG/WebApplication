using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// test custom exception serializerion.
        /// </summary>
        /// <remarks>
        ///     default Json serializer does not support serialization of all exception's properties, especially Data property.
        ///     so we build custom exception serializer (for response) which serialize reliable excpeiton's properties , according to runtime  environment
        /// </remarks>
        /// <response code="500">serialized exception</response>
        [Route("A")]
        [HttpGet]
        public IActionResult A()
        {
            try
            {
                throw new Exception("some exception");
            }
            catch (Exception ex)
            {
                var x = new DemoException { Name = "name", Exception = ex };
                return Ok(x);
            }
        }

        /// <summary>
        /// returns request's body.
        /// </summary>
        /// <remarks>
        ///     in asp .net , [FromBody] attribute expects json in reqeust's body as default,
        ///     therefore passing string in the body will not works because it is not a valid json format, even if
        ///     Content-Type (in header) is text/plain (for text).
        ///     we solved this by creating an Input Formatter which read the body as raw text if content type is text/plain (we select this
        ///     option in swagger).
        ///     Consumes("text/plain") attribute makes this header as default in swagger for this endpoint.
        /// </remarks>
        /// <response code="200">request's body</response>
        [Route("B")]
        [HttpPost]
        [Consumes("text/plain")]
        public string B([FromBody] string s)
        {
            return s;
        }

    }
}
