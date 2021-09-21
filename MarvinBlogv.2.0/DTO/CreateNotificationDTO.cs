using System;

namespace MarvinBlogv._2._0.DTO
{
    public class CreateNotificationDTO
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string ImageURL { get; set; }

        public string Type { get; set; }
        
        public  string Message { get; set; }
        
        public  string CreatedBy { get; set; }
        
        public  int UserId { get; set; }
        
        public int? PostId { get; set; }
    }
}