using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Notifications.Dal;
using Notifications.Mapper;
using Notifications.Repository;
using Notifications.Service.HubInterface;
using Notifications.Service.HubService;
using Notifications.Web.SignalR.Hub;
using Owin;

[assembly: OwinStartup(typeof(Notifications.Web.SignalR.Startup))]
namespace Notifications.Web.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfig = HubConfiguration();
            var container = RegisterAutoFac(hubConfig);
            app.UseCors(CorsOptions.AllowAll);
            app.UseAutofacMiddleware(container);
            app.MapSignalR(hubConfig);

            // To add custom HubPipeline modules, you have to get the HubPipeline
            // from the dependency resolver, for example:
            //var hubPipeline = hubConfig.Resolver.Resolve<IHubPipeline>();
            //hubPipeline.AddModule(new MyPipelineModule());
        }

        private HubConfiguration HubConfiguration()
        {
            return new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJSONP = true,
                EnableJavaScriptProxies = true
            };
        }

        private IContainer RegisterAutoFac(HubConfiguration hubConfig)
        {
            var builder = new ContainerBuilder();
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            RegisterServices(builder);
            RegisterHubsForService(builder, hubConfig);
            var container = builder.Build();
            hubConfig.Resolver = new AutofacDependencyResolver(container);

            return container;
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().As<IContext>().ExternallyOwned();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>().ExternallyOwned();
            builder.RegisterType<HubService>().As<IHubService>().ExternallyOwned();
            builder.RegisterType<NotificationFactory>().As<INotificationFactory>().ExternallyOwned();
        }

        private void RegisterHubsForService(ContainerBuilder builder, HubConfiguration hubConfig)
        {
            builder.Register(i => hubConfig.Resolver.Resolve<IConnectionManager>().GetHubContext<NotificationHub, INotificationHub>()).ExternallyOwned();
        }
    }
}