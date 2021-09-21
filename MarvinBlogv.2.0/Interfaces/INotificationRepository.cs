using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvinBlogv._2._0.Models;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface INotificationRepository
    {
        public Notification AddNotification(Notification notification);

        public void Delete(int id);

        public Notification Update(Notification notification);
        
        public Notification FindById(int id);
        
        public IEnumerable<Notification> GetUserNotificationByUserId(int userId);

        public int GetUserNotificationByUserIdCount(int userId);
    }
}
