using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Payloads.Request
{
    public class CreateBlogRequest
    {
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; } = null!;

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = null!;
        [JsonIgnore]
        public string UserId { get; set; } = null!;
    }
}
