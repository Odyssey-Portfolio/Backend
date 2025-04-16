using AutoMapper;
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
    public class CreateBlogHandler
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private CreateBlogRequest? _request;
        private Blog? _blog;
        private User? _user;
        public CreateBlogHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> Handle(CreateBlogRequest request)
        {
            try
            {
                _request = request;
                bool isUserValid = await CheckUserValidity();
                if (!isUserValid) return InvalidUserResponse();
                MapBlogRequestToBlog();
                SaveBlog();
                return CreateBlogSuccessResponse();
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }
        private async Task<bool> CheckUserValidity()
        {
            _user = await _userManager.FindByIdAsync(_request.UserId);
            if (_user != null) return true;
            return false;
        }
        private async void MapBlogRequestToBlog()
        {
            _blog = _mapper.Map<Blog>(_request);
            _blog.Id = Utils.GenerateEntityId<Blog>();
            _blog.Image = await FileUtils.IFormFileToBase64(_request.Image);
            _blog.User = _user;
        }
        private void SaveBlog()
        {
            _unitOfWork.BlogRepository.Insert(_blog);
            _unitOfWork.Save();
        }
        private ServiceResponse CreateBlogSuccessResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.CREATED,
                Message = "Successfully created a blog.",
            };
        }

        private ServiceResponse InvalidUserResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.BAD_REQUEST,
                Message = "Either the email or password is invalid. Please try again."
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
