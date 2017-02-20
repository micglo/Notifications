﻿using System;
using System.Windows.Threading;

namespace Notifications.Wpf.Utils
{
    public static class Execute
    {
        private static Action<Action> _executor = action => action();

        /// <summary>
        /// Initializes the framework using the current dispatcher.
        /// </summary>
        public static void InitializeWithDispatcher()
        {
#if SILVERLIGHT
        var dispatcher = Deployment.Current.Dispatcher;
#else
            var dispatcher = Dispatcher.CurrentDispatcher;
#endif
            _executor = action =>
            {
                if (dispatcher.CheckAccess())
                    action();
                else dispatcher.BeginInvoke(action);
            };
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void OnUIThread(this System.Action action)
        {
            _executor(action);
        }
    }
}