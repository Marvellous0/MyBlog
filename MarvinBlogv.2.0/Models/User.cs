using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarvinBlogv._2._0.Models
{
    public class User : BaseEntity
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string HashSalt { get; set; }
        public ICollection<Post> Posts { set; get; } = new HashSet<Post>();
        public List<Follower> Followers { get; set; } = new List<Follower>();

        public ICollection<UserRole> userRoles {set; get;} = new HashSet<UserRole>();
    }
}
