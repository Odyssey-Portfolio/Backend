using Microsoft.AspNetCore.Mvc;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Services;

namespace OdysseyPortfolio_BE.Controllers
{
    [Route("/blog")]
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;

        }
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreateBlogRequest request)
        {
            var result = _blogService.Create(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
