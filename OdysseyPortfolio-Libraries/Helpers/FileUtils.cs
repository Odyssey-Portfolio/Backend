using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Helpers
{
    internal class FileUtils
    {
        public static async Task<string> IFormFileToBase64(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.ToArray();
            var mimeType = file.ContentType;
            var base64String = Convert.ToBase64String(memoryStream.ToArray());
            return $"data:{mimeType};base64,{base64String}";
        }
    }
}
