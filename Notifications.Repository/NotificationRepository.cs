using System.Threading.Tasks;
using MongoDB.Driver;
using Notifications.Dal;
using Notifications.Domain;

namespace Notifications.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IContext _context;

        public NotificationRepository(IContext context)
        {
            _context = context;
        }

        public async Task Add(Notification notification)
            => await _context.GetCollection().InsertOneAsync(notification);

        public async Task ChangeStatus(string notificationId, string status)
        {
            var updateBuilder = Builders<Notification>.Update.Set(x => x.Status, GetStatus(status));
            await _context.GetCollection().UpdateOneAsync(x => x.Id.Equals(notificationId), updateBuilder);
        }


        private static DeliveryStatus GetStatus(string status)
        {
            switch (status)
            {
                case "Wysłany":
                    return DeliveryStatus.Sent;
                case "Powiadomiony":
                    return DeliveryStatus.Notified;
                case "Odczytany":
                    return DeliveryStatus.Delivered;
                case "Usunięty":
                    return DeliveryStatus.Deleted;
                default:
                    return DeliveryStatus.Obsolete;
            }
        }
    }
}