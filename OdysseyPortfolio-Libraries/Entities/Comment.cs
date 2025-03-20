﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyPortfolio_Libraries.Entities
{
    public class Comment
    {
        public string Id { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsDisabled { get; set; }
        public string DisabledReason { get; set; } = null!;
        public string BlogId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Blog Blog { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}
