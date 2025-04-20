using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OdysseyPortfolio_Libraries.DTOs;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Helpers;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Services;
using static OdysseyPortfolio_Libraries.Helpers.HttpUtils;
using static OdysseyPortfolio_Libraries.Services.Implementations.UserService.LoginService;
using LoginRequest = OdysseyPortfolio_Libraries.Payloads.Request.LoginRequest;

namespace OdysseyPortfolio_BE.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _userService.Login(request);
            var loggedInUser = result.ReturnData as LoggedInUserDto;
            SetTokensInsideCookie(new SetTokensInsideCookieOptions()
            {
                HttpContext = HttpContext,
                Token = loggedInUser.Token,
                TokenType = TokenTypes.ACCESS_TOKEN
            });
            loggedInUser.Token = null;
            return StatusCode(result.StatusCode, result);
        }
    }
}
