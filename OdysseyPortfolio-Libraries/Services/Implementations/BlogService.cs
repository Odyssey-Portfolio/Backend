using AutoMapper;
using Azure.Core;
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

namespace OdysseyPortfolio_Libraries.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private CreateBlogHandler? _createBlogHandler;
        private GetBlogsHandler? _getBlogsHandler;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;   
            InitializeServices();
        }
        public BlogServiceResponse Create(CreateBlogRequest request)
        {
            return _createBlogHandler.Handle(request);
        }

        public BlogServiceResponse Get(GetBlogsRequest request)
        {
            return _getBlogsHandler.Handle(request);    
        }
        private void InitializeServices()
        {
            _createBlogHandler = new CreateBlogHandler(_unitOfWork,_mapper);
            _getBlogsHandler = new GetBlogsHandler(_unitOfWork, _mapper);
        }
    }
}
