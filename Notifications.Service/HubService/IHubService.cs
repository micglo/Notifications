using System.Threading.Tasks;
using Notifications.Model;

namespace Notifications.Service.HubService
{
    public interface IHubService
    {
        Task ProcessMessage(string user, string senderClientId, Message message);
        Task ChangeStatus(string notificationId, string status);
    }
}