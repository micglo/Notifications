using System;
using System.Windows;
using System.Windows.Input;
using Notifications.Service.HubClientService;
using Notifications.Wpf.Utils;

namespace Notifications.Wpf.ViewModel
{
    public class NotifyIconViewModel : Observable
    {
        private readonly IHubClientService _client;
        private bool _connectionActive;

        public NotifyIconViewModel()
        {
            _client = new HubClientService();
            Disconnect = new DelegateCommand(OnDisconnectExecute, OnDisconnectCanExecute);
            Connect = new DelegateCommand(OnConnectExecute, OnConnectCanExecute);
            _client.ConnectionEvent += ClientOnConnectionEvent;
            _connectionActive = _client.Connected;
        }

        #region Properties

        public ICommand ExitApplication
        {
            get
            {
                var exec = (Action<object>)delegate
                {
                    Application.Current.Shutdown();
                };
                return new DelegateCommand(exec);
            }
        }

        public DelegateCommand Disconnect { get; }

        public DelegateCommand Connect { get; }

        public bool ConnectionActive
        {
            get { return _connectionActive; }
            set
            {
                SetProperty(ref _connectionActive, value);
                Disconnect.RaiseCanExecuteChanged();
                Connect.RaiseCanExecuteChanged();
            }
        }

        #endregion



        #region Helpers

        private bool OnConnectCanExecute(object arg)
            => !ConnectionActive;

        private void OnConnectExecute(object obj)
            => _client.Start();

        private bool OnDisconnectCanExecute(object arg)
            => ConnectionActive;

        private void OnDisconnectExecute(object obj)
            => _client.Stop();

        private void ClientOnConnectionEvent(bool connected)
            => Execute.OnUIThread(() => SetConnectionActive(connected));

        private void SetConnectionActive(bool connected)
        {
            if (ConnectionActive != connected)
                ConnectionActive = connected;
        }

        #endregion
    }
}