using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Payloads.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Services.Implementations.UserService
{

    public class LogoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public LogoutService(IHttpContextAccessor httpContextAccessor, ILogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ServiceResponse> Handle()
        {
            try
            {
                DeleteJwtCookie();
                return new ServiceResponse
                {
                    StatusCode = ResponseCodes.SUCCESS,
                    Message = "Logged out successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while logging out.");
                return new ServiceResponse
                {
                    StatusCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                    Message = $"Something went wrong while logging out. {ex.Message}"
                };
            }
        }

        private void DeleteJwtCookie()
        {            
            var response = _httpContextAccessor.HttpContext?.Response;
            if (response == null) return;                                    
            response.Cookies.Append("accessToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });
        }
    }
}

