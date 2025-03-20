using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string PositionOnBlog { get; set; } = null!;
        public string BlogId { get; set; } = null!;
        public Blog Blog { get; set; } = null!;
    }
}
