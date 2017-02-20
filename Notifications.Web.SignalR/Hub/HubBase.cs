using Autofac;
using Notifications.Service.HubService;

namespace Notifications.Web.SignalR.Hub
{
    public abstract class HubBase<T> : Microsoft.AspNet.SignalR.Hub<T> where T : class 
    {
        #region Fields

        protected readonly IHubService Service;
        private readonly ILifetimeScope _hubLifetimeScope;

        #endregion


        protected HubBase(ILifetimeScope lifetimeScope)
        {
            _hubLifetimeScope = lifetimeScope.BeginLifetimeScope();
            Service = _hubLifetimeScope.Resolve<IHubService>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _hubLifetimeScope != null)
                _hubLifetimeScope.Dispose();

            base.Dispose(disposing);
        }
    }
}