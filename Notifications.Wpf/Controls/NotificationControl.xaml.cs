using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;

namespace Notifications.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl
    {
        #region Dependency property

        public static readonly DependencyProperty SenderProperty =
            DependencyProperty.Register("Sender",
                typeof(string),
                typeof(NotificationControl),
                new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty SubjectProperty =
            DependencyProperty.Register("Subject",
                typeof(string),
                typeof(NotificationControl),
                new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty NotificationIdProperty =
            DependencyProperty.Register("NotificationId",
                typeof(string),
                typeof(NotificationControl),
                new FrameworkPropertyMetadata(""));

        public string Sender
        {
            get { return (string)GetValue(SenderProperty); }
            set { SetValue(SenderProperty, value); }
        }

        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }

        public string NotificationId
        {
            get { return (string)GetValue(NotificationIdProperty); }
            set { SetValue(NotificationIdProperty, value); }
        }

        #endregion

        public NotificationControl()
        {
            InitializeComponent();
            TaskbarIcon.AddBalloonClosingHandler(this, OnNotificationClosing);
        }

        private void OnNotificationClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            Popup pp = (Popup)Parent;
            pp.IsOpen = false;
        }
    }
}
