using AutoMapper;
using Microsoft.Identity.Client;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.DTOs;
using OdysseyPortfolio_Libraries.Entities;
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
    public class GetBlogsHandler
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private GetBlogsRequest? _request;
        private IEnumerable<Blog>? _blogs;
        private List<GetBlog>? _getBlogs;

        public GetBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> Handle(GetBlogsRequest request)
        {
            try
            {
                _request = request;
                _blogs = _unitOfWork.BlogRepository.Get();
                MapBlogsToGetBlogs();
                ApplyRequestParameters();
                return GetBlogsSuccessResponse();
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }
        private void MapBlogsToGetBlogs()
        {
            _getBlogs = new List<GetBlog>();
            foreach (var blog in _blogs)
            {
                var getBlog = _mapper.Map<GetBlog>(blog);
                _getBlogs.Add(getBlog);
            }
        }
        private void ApplyRequestParameters()
        {
            _blogs = _blogs
               .Skip((_request.PageNumber - 1) * _request.PageSize)
               .Take(_request.PageSize)
               .ToList();
        }
        private ServiceResponse GetBlogsSuccessResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.SUCCESS,
                Message = "Successfully retrieved all blogs.",
                ReturnData = _getBlogs
            };
        }
        private ServiceResponse InternalServerErrorResponse(Exception ex)
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.INTERNAL_SERVER_ERROR,
                Message = $"Something went wrong on the server side. {ex.Message}"
            };
        }
    }
}
