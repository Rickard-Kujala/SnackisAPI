using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public bool AbuseReport { get; set; }
        public string PostParent { get; set; }//post id från den post man svarar på
    }
    
    
}

