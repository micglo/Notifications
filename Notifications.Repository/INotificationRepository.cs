using System.Threading.Tasks;
using Notifications.Domain;

namespace Notifications.Repository
{
    public interface INotificationRepository
    {
        Task Add(Notification notification);
        Task ChangeStatus(string notificationId, string status);
    }
}