using System.Collections.Generic;
using MarvinBlogv._2._0.DTO;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class HomeVM
    {
        public List<PostDTo> Posts { get; set; }
        public List<Category> Categories { get; set; }
        
        public List<UserDto> Users { get; set; }
    }
}