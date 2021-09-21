using System;
namespace MarvinBlogv._2._0.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt {get;set;}
        public DateTime? LastModifiedOn { get; set; }
        public int Id {get;set;}
        public string CreatedBy { get; set; }
    }
}