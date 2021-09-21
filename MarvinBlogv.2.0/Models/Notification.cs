using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        
        public User User { get; set; }
        
        public  int UserId { get; set; }

        public string ImageURL { get; set; }

        public  Post Post { get; set; }

        public string Type { get; set; }

        public int? PostId { get; set; }
    }
}
