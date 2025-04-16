using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Helpers;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Payloads.Response;
using OdysseyPortfolio_Libraries.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Services.Implementations.BlogService
{
    public class BlogService : IBlogService
    {
        private CreateBlogHandler? _createBlogHandler;
        private GetBlogsHandler? _getBlogsHandler;
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            InitializeServices();
        }
        public async Task<ServiceResponse> Create(CreateBlogRequest request)
        {
            var result = await _createBlogHandler.Handle(request);
            return result;
        }

        public async Task<ServiceResponse> Get(GetBlogsRequest request)
        {
            var result = await _getBlogsHandler.Handle(request);
            return result;
        }
        private void InitializeServices()
        {
            _createBlogHandler = new CreateBlogHandler(_unitOfWork, _userManager, _mapper);
            _getBlogsHandler = new GetBlogsHandler(_unitOfWork, _mapper);
        }
    }
}
