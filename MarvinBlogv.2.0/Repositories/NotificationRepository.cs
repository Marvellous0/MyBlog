using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MarvinBlogv._2._0.Services
{
    public class NotificationRepository : INotificationRepository
    {
    private readonly BlogDbContext _dbContext;
    
    public NotificationRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        public Notification AddNotification(Notification notification)
        {
            _dbContext.Add(notification);
            _dbContext.SaveChanges();
            return notification;
        }

        public void Delete(int id)
        {
            var notification = FindById(id);
            {
                if (notification != null)
                {
                    _dbContext.Notifications.Remove(notification);
                    _dbContext.SaveChanges();
                }
            }
        }

        public Notification Update(Notification notification)
        {
            _dbContext.Notifications.Update(notification);
            _dbContext.SaveChanges();
            return notification;
        }

        public Notification FindById(int id)
        {
            return _dbContext.Notifications.Find(id);
        }

        public IEnumerable<Notification> GetUserNotificationByUserId(int userId)
        {
            return _dbContext.Notifications.Include(n => n.Post).Where(n => n.UserId == userId).ToList();
        }

        public int GetUserNotificationByUserIdCount(int userId)
        {
            return _dbContext.Notifications.Include(n => n.Post).Where(n => n.UserId == userId).Count();
        }
    }
}