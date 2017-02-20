using System;
using MongoDB.Bson;
using Notifications.Domain;
using Notifications.Model;

namespace Notifications.Mapper
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification CreateNotification(Message message, string sender, string recipient, DeliveryStatus status = DeliveryStatus.Sent)
            => new Notification
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Sender = sender,
                Recipient = recipient,
                Status = status,
                DateSent = DateTime.Now,
                Subject = message.Subject,
                Text = message.Text
            };

        public NotificationDto CreateNotification(Notification notification)
            => new NotificationDto
            {
                Id = notification.Id,
                Sender = notification.Sender,
                Recipient = notification.Recipient,
                Status = GetStatus(notification.Status),
                DateSent = notification.DateSent,
                DateNotified = notification.DateNotified,
                DateDelivered = notification.DateDelivered,
                Subject = notification.Subject,
                Text = notification.Text
            };

        public NotificationObservable CreateNotification(NotificationDto notification)
            => new NotificationObservable
            {
                Id = notification.Id,
                Sender = notification.Sender,
                Status = notification.Status,
                DateSent = notification.DateSent,
                Subject = notification.Subject,
                Text = notification.Text
            };

        private string GetStatus(DeliveryStatus status)
        {
            switch (status)
            {
                case DeliveryStatus.Sent:
                    return "Wysłany";
                case DeliveryStatus.Notified:
                    return "Powiadomiony";
                case DeliveryStatus.Delivered:
                    return "Odczytany";
                case DeliveryStatus.Deleted:
                    return "Usunięty";
                default:
                    return "Przedawniony";
            }
        }
    }
}