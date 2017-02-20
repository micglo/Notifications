using Notifications.Domain;
using Notifications.Model;

namespace Notifications.Mapper
{
    public interface INotificationFactory
    {
        Notification CreateNotification(Message message, string sender, string recipient, DeliveryStatus status = DeliveryStatus.Sent);
        NotificationDto CreateNotification(Notification notification);
        NotificationObservable CreateNotification(NotificationDto notification);
    }
}