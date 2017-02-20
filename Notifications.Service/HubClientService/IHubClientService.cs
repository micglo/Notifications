using System;
using Notifications.Model;

namespace Notifications.Service.HubClientService
{
    public interface IHubClientService
    {
        event Action<NotificationObservable> RecievedNotificationEvent;
        event Action<bool> ConnectionEvent;
        bool Connected { get; }

        void Start();
        void Stop();

        void ConfirmNotified(string notificationId);
        void ConfirmRead(string notificationId);
    }
}