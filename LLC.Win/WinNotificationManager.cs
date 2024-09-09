using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopNotifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace LLC.Win
{
    internal class WinNotificationManager : INotificationManager
    {
        public void Dispose()
        {
            // TODO release managed resources here
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public Task ShowNotification(Notification notification, DateTimeOffset? expirationTime = null)
        {
            var toast = new ToastContentBuilder();

            if (notification.Title is not null)
            {
                toast.AddText(notification.Title, hintStyle: AdaptiveTextStyle.Header);
            }

            if (notification.Body is not null)
            {
                toast.AddText(notification.Body, hintStyle: AdaptiveTextStyle.Body);
            }

            if (notification.BodyImagePath is not null)
            {
                toast.AddAppLogoOverride(new Uri(notification.BodyImageAltText));
            }

            if (notification.Buttons is not null)
            {
                notification.Buttons.ForEach(x =>
                {

                });
            }


            toast.Show(t =>
            {
                if (expirationTime is not null)
                {
                    t.ExpirationTime = expirationTime;
                }
            });


            return Task.CompletedTask;

        }

        public Task HideNotification(Notification notification)
        {
            return Task.CompletedTask;

        }

        public Task ScheduleNotification(Notification notification, DateTimeOffset deliveryTime,
            DateTimeOffset? expirationTime = null)
        {
            return Task.CompletedTask;
        }

        public string? LaunchActionId { get; }
        public NotificationManagerCapabilities Capabilities { get; }
        public event EventHandler<NotificationActivatedEventArgs>? NotificationActivated;
        public event EventHandler<NotificationDismissedEventArgs>? NotificationDismissed;
    }
}
