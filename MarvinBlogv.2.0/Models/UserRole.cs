namespace MarvinBlogv._2._0.Models
{
    public class UserRole : BaseEntity
    {
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
