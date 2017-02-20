using System;

namespace Notifications.Model
{
    public class NotificationDto
    {
        public string Id { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public string Status { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateNotified { get; set; }
        public DateTime? DateDelivered { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}