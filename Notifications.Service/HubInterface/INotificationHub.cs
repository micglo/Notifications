using System.Threading.Tasks;
using Notifications.Model;

namespace Notifications.Service.HubInterface
{
    public interface INotificationHub
    {
        Task Notify(NotificationDto notification);
    }
}