using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Helpers
{
    public static class Utils
    {
        public static string GenerateEntityId<T>()
        {
            string typePrefix = typeof(T).Name.Substring(0, 1).ToUpper(); 
            string dateTimePart = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"); 
            return $"{typePrefix}_{dateTimePart}";
        }
    }
}
