using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Payloads.Request
{
    public class GetBlogsRequest
    {        
        public string? Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }        
    }
}
