using System.Windows;
using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using Notifications.Model;
using Notifications.Service.HubClientService;
using Notifications.Wpf.Controls;
using Notifications.Wpf.Utils;

namespace Notifications.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private TaskbarIcon _notifyIcon;
        private IHubClientService _clientService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Execute.InitializeWithDispatcher();
            //var kernel = NinjectInitializator.InitializeNinjectKernel();
            _clientService = new HubClientService();

            if (!_clientService.Connected)
                Current.Shutdown();

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            _clientService.RecievedNotificationEvent += ClientServiceOnRecievedNotificationEvent;
        }

        private void ClientServiceOnRecievedNotificationEvent(NotificationObservable notificationObservable)
        {
            Current.Dispatcher.Invoke(delegate
            {
                var notification = new NotificationControl
                {
                    Sender = notificationObservable.Sender.Substring(5) + " - " + notificationObservable.DateSent,
                    Subject = notificationObservable.Subject,
                    NotificationId = notificationObservable.Id
                };

                _notifyIcon.ShowCustomBalloon(notification, PopupAnimation.Slide, 6000);
                _clientService.ConfirmNotified(notificationObservable.Id);
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
