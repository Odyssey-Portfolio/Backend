using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Payloads.Request
{
    public class CreateBlogRequest
    {        
        public IFormFile Image { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        [JsonIgnore]
        public string UserId { get; set; } = null!;
    }
}
