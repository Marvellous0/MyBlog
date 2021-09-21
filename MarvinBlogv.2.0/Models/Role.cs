using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> userRoles { set; get; } = new HashSet<UserRole>();
    }
}
