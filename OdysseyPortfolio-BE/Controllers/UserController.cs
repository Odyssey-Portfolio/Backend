﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Helpers;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Services;
using static OdysseyPortfolio_Libraries.Helpers.HttpUtils;
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
            SetTokensInsideCookie(new SetTokensInsideCookieOptions()
            {
                HttpContext = HttpContext,
                Token = (string)result.ReturnData,
                TokenType = TokenTypes.REFRESH_TOKEN
            });

            return StatusCode(result.StatusCode, result);
        }
    }
}
