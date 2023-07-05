using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class WordsController : ControllerBase
    {
        private readonly string[] someWords = new string[] { "Hello", "World", "Foo", "Bar" };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(someWords);
        }
    }
}