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
    public class DeleteBlogRequest
    {
        [Required(ErrorMessage = "The Id of the Blog is required.")]
        public string Id { get; set; } = null!;

        [JsonIgnore]
        public string UserId { get; set; } = null!;
    }
}
