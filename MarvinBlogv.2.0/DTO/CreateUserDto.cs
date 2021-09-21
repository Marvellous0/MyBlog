using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.DTO
{
    public class CreateUserDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
