using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNet.SignalR.Client;
using Notifications.Mapper;
using Notifications.Model;

namespace Notifications.Service.HubClientService
{
    public class HubClientService : IHubClientService
    {
        #region Fields

        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;
        private readonly INotificationFactory _factory;

        #endregion

        #region Properties

        public event Action<NotificationObservable> RecievedNotificationEvent;

        public event Action<bool> ConnectionEvent;

        public ConnectionState State => _hubConnection.State;

        public bool Connected { get; set; }

        #endregion

        public HubClientService()
        {
            _factory = new NotificationFactory();
            Start();
        }


        public void Start()
        {
            var hubAddress = ConfigurationManager.AppSettings["SignalRServerUrl"];
            _hubConnection = new HubConnection(hubAddress)
            {
                Credentials = CredentialCache.DefaultCredentials
            };

            _hubProxy = _hubConnection.CreateHubProxy("NotificationHub");
            _hubProxy.On<NotificationDto>("Notify", OnGetNotification);
            StartHubInternal();

            _hubConnection.StateChanged += HubConnectionOnStateChanged;
            _hubConnection.Error += HubConnectionOnError;
        }

        public void Stop()
        {
            _hubConnection.Stop();
            _hubConnection.Dispose();
        }

        public void ConfirmNotified(string notificationId)
        {
            if (State == ConnectionState.Connected)
                _hubProxy.Invoke("ChangeStatus", notificationId, "Powiadomiony");
        }

        public void ConfirmRead(string notificationId)
        {
            if (State == ConnectionState.Connected)
                _hubProxy.Invoke("ChangeStatus", notificationId, "Odczytany");
        }



        #region Helpers


        private void StartHubInternal()
        {
            try
            {
                _hubConnection.Start().Wait();
                Connected = true;
                ConnectionEvent?.Invoke(true);
            }
            catch (Exception ex)
            {
                Connected = false;
                var logMsg = "Próba połączenia z serwisem Hermes. " + ex.Message;
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(logMsg, EventLogEntryType.Error);
                }
            }
        }

        private void OnGetNotification(NotificationDto notification)
        {
            if (State == ConnectionState.Connected)
                RecievedNotificationEvent?.Invoke(_factory.CreateNotification(notification));
        }

        private void HubConnectionOnStateChanged(StateChange stateChange)
            => ConnectionEvent?.Invoke(stateChange.NewState == ConnectionState.Connected);

        private static void HubConnectionOnError(Exception exception)
        {
            Console.WriteLine("Wystąpił błąd. Zgłoś go do DSI.");
            Console.WriteLine("{0}", exception.Message);
        }

        #endregion
    }
}