using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Helpers;
using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Payloads.Response;
using OdysseyPortfolio_Libraries.Repositories;
using System;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Services.Implementations.BlogService
{
    public class UpdateBlogHandler
    {
        private readonly UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private UpdateBlogRequest? _request;
        private Blog? _blog;
        private User? _user;

        public UpdateBlogHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> Handle(UpdateBlogRequest request)
        {
            try
            {
                _request = request;
                bool isUserValid = await CheckUserValidity();
                if (!isUserValid) return InvalidUserResponse();

                bool isBlogValid = CheckBlogValidity();
                if (!isBlogValid) return BlogNotFoundResponse();

                _mapper.Map(_request,_blog); 
                SaveChanges();
                return UpdateBlogSuccessResponse();
            }
            catch (Exception ex)
            {
                return InternalServerErrorResponse(ex);
            }
        }

        private async Task<bool> CheckUserValidity()
        {
            _user = await _userManager.FindByIdAsync(_request.UserId);
            return _user != null;
        }

        private bool CheckBlogValidity()
        {
            _blog =  _unitOfWork.BlogRepository.GetByID(_request.Id);
            return _blog != null;
        }
        

        private void SaveChanges()
        {
            _unitOfWork.BlogRepository.Update(_blog);
            _unitOfWork.Save();
        }

        private ServiceResponse UpdateBlogSuccessResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.SUCCESS,
                Message = "Successfully updated the blog.",
            };
        }

        private ServiceResponse InvalidUserResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.BAD_REQUEST,
                Message = "Invalid user. Please check and try again."
            };
        }

        private ServiceResponse BlogNotFoundResponse()
        {
            return new ServiceResponse()
            {
                StatusCode = ResponseCodes.NOT_FOUND,
                Message = "Blog not found. Please check the BlogId."
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
