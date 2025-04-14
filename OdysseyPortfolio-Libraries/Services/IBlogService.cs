﻿using OdysseyPortfolio_Libraries.Payloads.Request;
using OdysseyPortfolio_Libraries.Payloads.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Services
{
    public interface IBlogService
    {
        public BlogServiceResponse Create(CreateBlogRequest request);
        public BlogServiceResponse Get(GetBlogsRequest request);
    }
}
