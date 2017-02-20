using System.Threading.Tasks;
using Autofac;
using Notifications.Service.HubInterface;

namespace Notifications.Web.SignalR.Hub
{
    public class NotificationHub : HubBase<INotificationHub>
    {
        public NotificationHub(ILifetimeScope lifetimeScope) : base(lifetimeScope)
        {
        }

        public async Task ChangeStatus(string notificationId, string status)
            => await Service.ChangeStatus(notificationId, status);
    }
}