using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Payloads.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Services.Implementations.UserService
{
    public class UserService : IUserService
    {
        private LoginService _loginService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            InitializeServices();
        }
        public ServiceResponse Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }
        private void InitializeServices()
        {
            _loginService = new LoginService(_userManager, _roleManager, _configuration);
        }

    }
}
