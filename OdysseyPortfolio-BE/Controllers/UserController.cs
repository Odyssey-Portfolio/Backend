using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Services;
using LoginRequest = OdysseyPortfolio_Libraries.Payloads.Request.LoginRequest;

namespace OdysseyPortfolio_BE.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _userService.Login(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
