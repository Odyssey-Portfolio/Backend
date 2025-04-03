using AutoMapper;
using OdysseyPortfolio_Libraries.Payloads.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBlogRequest, Entities.Blog>()
                .ReverseMap();

        }
    }
}
