using System.Threading.Tasks;
using Autofac;
using Notifications.Model;
using Notifications.Service.HubInterface;

namespace Notifications.Web.SignalR.Hub
{
    public class MessageHub : HubBase<IMessageHub>
    {
        public MessageHub(ILifetimeScope lifetimeScope) : base(lifetimeScope)
        {
        }

        public async Task Send(Message message)
        {
            var user = Context.User.Identity.Name;
            var senderClientId = Context.ConnectionId;
            await Service.ProcessMessage(user, senderClientId, message);
        }
    }
}