using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Entities
{
    public class Blog
    {
        public string Id { get; set; } = null!;
        public string Image { get; set; } = "";
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public User User { get; set; } = null!; 
        public ICollection<Comment> Comments { get; set; } = new List<Comment>()!;
        public ICollection<Image> Images { get; set; } = new List<Image>()!;
    }
}
