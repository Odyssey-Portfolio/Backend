using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Payloads.Response
{
    public class ServiceResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public object ReturnData { get; set; } = null!; 
    }
}
