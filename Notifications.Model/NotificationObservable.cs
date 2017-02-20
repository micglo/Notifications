using System;
using Notifications.Wpf.Utils;

namespace Notifications.Model
{
    public class NotificationObservable : Observable
    {
        #region Fields

        private string _id;
        private string _status;
        private DateTime _dateSent;
        private string _sender;
        private string _subject;
        private string _text;

        #endregion


        #region Properties

        public string Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        public DateTime DateSent
        {
            get { return _dateSent; }
            set
            {
                SetProperty(ref _dateSent, value);
            }
        }

        public string Sender
        {
            get { return _sender; }
            set
            {
                SetProperty(ref _sender, value);
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                SetProperty(ref _subject, value);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
            }
        }

        #endregion
    }
}