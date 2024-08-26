using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AppKit;
using DesktopNotifications;
using Foundation;
using ObjCRuntime;
using UserNotifications;

namespace LLC.Mac;

public class LinkedUNNotificationRequest : IDisposable
{
    private UNNotificationRequest? Request { get; set; }
    private string Id { get; }
    private Notification? Notification { get; set; }

    public LinkedUNNotificationRequest(UNNotificationRequest request, string id, Notification notification)
    {
        Request = request;
        Id = id;
        Notification = notification;
    }

    public void Dispose()
    {
        Request?.Dispose();
        Notification = null;
    }
}

public class MacNotificationManager : INotificationManager
{
    private UNUserNotificationCenter? NotificationCenter { get; set; }
    private UNNotificationSettings Ustawienia { get; set; }

    private bool? _canShowNotification;

    private bool CanShowNotification
    {
        get => _canShowNotification ??= Ustawienia.AuthorizationStatus == UNAuthorizationStatus.Authorized &&
                                        Ustawienia.AlertSetting == UNNotificationSetting.Enabled;
        set => _canShowNotification = value;
    }

    public MacNotificationManager()
    {
        NotificationCenter ??= UNUserNotificationCenter.Current;
        NotificationCenter?.GetNotificationSettings(settings => { Ustawienia = settings; });
    }

    public Task Initialize()
    {
        // NotificationsNative = new ();
        Capabilities = NotificationManagerCapabilities.BodyText | NotificationManagerCapabilities.Audio |
                       NotificationManagerCapabilities.Icon;
        return Task.CompletedTask;
    }

    async Task CompletionHandler(bool granted, Func<UNUserNotificationCenter, Task> action, NSError? error)
    {
        if (granted)
        {
            await action(NotificationCenter);
        }

        if (CanShowNotification != granted)
        {
            CanShowNotification = granted;
        }

        if (error != null)
        {
            throw new Exception(error.LocalizedDescription);
        }
    }

    private async Task RequestPermissionAndSendOrBlockNotification(Func<UNUserNotificationCenter, Task> action)
    {
        if (CanShowNotification)
        {
            await CompletionHandler(true, action, null);
            return;
        }


        if (NotificationCenter != null)
        {
            Tuple<bool, NSError?> response = await NotificationCenter.RequestAuthorizationAsync(
                UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge |
                UNAuthorizationOptions.Sound);
            await CompletionHandler(response.Item1, action, response.Item2);
        }
    }


    public async Task ShowNotification(Notification not, DateTimeOffset? expirationTime = null)
    {
        await RequestPermissionAndSendOrBlockNotification(async (nc) =>
        {
            string cheksumOfNotificationObject = Guid.NewGuid().ToString(); //not.GetHashCode().ToString();
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);
            var content = new UNMutableNotificationContent
            {
                Title = not.Title ?? string.Empty,
                Body = not.Body ?? string.Empty,
                Sound = UNNotificationSound.Default,
                CategoryIdentifier = "LLC",
            };
            var request =
                UNNotificationRequest.FromIdentifier(cheksumOfNotificationObject, content, trigger);
            await nc.AddNotificationRequestAsync(request);
            // NotificationsNative.Add(cheksumOfNotificationObject, request);
            NotificationActivated?.Invoke(this, new NotificationActivatedEventArgs(not, cheksumOfNotificationObject));
        });
    }

    public Task HideNotification(Notification not)
    {
        // string cheksumOfNotificationObject = not.GetHashCode().ToString();
        // var content = NotificationsNative[cheksumOfNotificationObject];
        //
        // UNUserNotificationCenter.Current.RemoveDeliveredNotifications(new[] {content});
        // NotificationDismissed?.Invoke(this, new NotificationDismissedEventArgs(notification, NotificationDismissReason.Application));
        // Notifications.Remove(notification);
        return Task.CompletedTask;
    }

    public async Task ScheduleNotification(Notification not, DateTimeOffset deliveryTime,
        DateTimeOffset? expirationTime = null)
    {
        await RequestPermissionAndSendOrBlockNotification(async (nc) =>
        {
            var id = Guid.NewGuid().ToString();
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);
            var content = new UNMutableNotificationContent
            {
                Title = not.Title ?? string.Empty,
                Body = not.Body ?? string.Empty,
                Sound = UNNotificationSound.Default,
            };
            var request =
                UNNotificationRequest.FromIdentifier(id, content, trigger);
            await nc.AddNotificationRequestAsync(request);
            // Notifications.Add(not, request);
            NotificationActivated?.Invoke(this, new NotificationActivatedEventArgs(not, id));
        });
    }

    public string? LaunchActionId { get; } = "LLC.Mac";
    public NotificationManagerCapabilities Capabilities { get; private set; }
    public event EventHandler<NotificationActivatedEventArgs>? NotificationActivated;
    public event EventHandler<NotificationDismissedEventArgs>? NotificationDismissed;

    public void Dispose()
    {
        NotificationCenter?.Dispose();
    }
}