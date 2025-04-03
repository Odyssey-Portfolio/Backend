using AutoMapper;
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
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Blog? _blog;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public BlogServiceResponse Create(CreateBlogRequest request)
        {
            try
            {
                _blog = _mapper.Map<Blog>(request);
                SaveBlog();
                return CreateBlogSuccessResponse();
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }
        private void SaveBlog()
        {
            _blog.Id = Utils.GenerateEntityId<Blog>();
            _unitOfWork.BlogRepository.Insert(_blog);
            _unitOfWork.Save();
        }
        private BlogServiceResponse CreateBlogSuccessResponse()
        {
            return new BlogServiceResponse()
            {
                StatusCode = ResponseCodes.CREATED,
                Message = "Successfully created a blog.",
            };
        }
        private BlogServiceResponse InternalServerErrorResponse(Exception ex)
        {
            return new BlogServiceResponse()
            {
                StatusCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                Message = $"Something went wrong on the server side. {ex.Message}"
            };
        }
    }
}
