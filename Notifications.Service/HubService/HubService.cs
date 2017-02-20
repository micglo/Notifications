using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Notifications.Mapper;
using Notifications.Model;
using Notifications.Repository;
using Notifications.Service.HubInterface;

namespace Notifications.Service.HubService
{
    public class HubService : IHubService
    {
        private readonly INotificationFactory _factory;
        private readonly INotificationRepository _repository;
        private readonly IHubContext<INotificationHub> _notificationHub;

        public HubService(INotificationFactory factory, INotificationRepository repository, IHubContext<INotificationHub> notificationHub)
        {
            _factory = factory;
            _repository = repository;
            _notificationHub = notificationHub;
        }

        public async Task ProcessMessage(string user, string senderClientId, Message message)
        {
            var recipients = GetRecipients(message.Recipients);

            foreach (var recipient in recipients)
            {
                var notification = _factory.CreateNotification(message, user, recipient);
                await _repository.Add(notification);
                var notificationDto = _factory.CreateNotification(notification);
                //await _notificationHub.Clients.All.Notify(notificationDto);
                await _notificationHub.Clients.AllExcept(senderClientId).Notify(notificationDto);
            }
        }

        public async Task ChangeStatus(string notificationId, string status)
            => await _repository.ChangeStatus(notificationId, status);



        private static IEnumerable<string> GetRecipients(string recipients)
            => recipients.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToList();
    }
}