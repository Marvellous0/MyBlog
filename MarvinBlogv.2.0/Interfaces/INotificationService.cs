using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Models;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface INotificationService
    {
        public Notification AddNotification(CreateNotificationDTO createNotificationDto);

        public Notification UpdateNotification(CreateNotificationDTO updateDto);

        public void Delete(int id);

        public Notification FindById(int id);

        public IEnumerable<Notification> GetNotificationByUserId(int userId);

        public int GetUserNotificationByUserIdCount(int userId);
    }
}
