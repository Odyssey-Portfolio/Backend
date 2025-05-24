using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private RegisterService _registerService;   
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            InitializeServices();
        }
        public async Task<ServiceResponse> Login(LoginRequest request)
        {
            var result = await _loginService.Handle(request);
            return result;
        }
        public async Task<ServiceResponse> Register(RegisterRequest request)
        {
            var result = await _registerService.Handle(request);
            return result;
        }
        private void InitializeServices()
        {            
            _loginService = new LoginService(_userManager, _roleManager, _configuration,_logger);
            _registerService = new RegisterService(_userManager, _roleManager, _configuration,_mapper, _logger);
        }    
    }
}
