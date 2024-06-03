using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AppKit;
using DesktopNotifications;
using Foundation;
using ObjCRuntime;
using UserNotifications;

namespace LLC.Mac;


public class LinkedUNNotificationRequest  : IDisposable
{
    private UNNotificationRequest? Request { get; set; }
    private string Id { get;  }
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
    private UNUserNotificationCenter? _notificationCenter;

    // private Dictionary<string, UNNotificationRequest> NotificationsNative { get; set; }
    

    // public void Dispose()
    // {
    //     foreach (var nsUserNotification in NotificationsNative)
    //     {
    //         nsUserNotification.Value.Dispose();
    //     }
    //     NotificationsNative.Clear();
    // }

    public Task Initialize()
    {
        // NotificationsNative = new ();
        Capabilities = NotificationManagerCapabilities.None;
        return Task.CompletedTask;
    }

    public async Task RequestPermissionAndSendOrBlockNotification(Func<UNUserNotificationCenter,Task> action)
    {
        _notificationCenter ??= UNUserNotificationCenter.Current;
        
        var settings = await _notificationCenter.GetNotificationSettingsAsync();
        if (settings is { AuthorizationStatus: UNAuthorizationStatus.Authorized, AlertSetting: UNNotificationSetting.Enabled })
        {
            await action(_notificationCenter);
        }
        async void CompletionHandler(bool granted, NSError error)
        {
            if (granted)
            {
                await action(_notificationCenter);
            }
        }
        _notificationCenter?.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound, CompletionHandler);
    }
    public async Task ShowNotification(Notification not, DateTimeOffset? expirationTime = null)
    {
        await RequestPermissionAndSendOrBlockNotification(async (nc) =>
        {
            string cheksumOfNotificationObject = not.GetHashCode().ToString();
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);
            var content = new UNMutableNotificationContent
            {
                Title = not.Title ?? string.Empty,
                Body = not.Body ?? string.Empty,
                Sound = UNNotificationSound.Default,
                InterruptionLevel = UNNotificationInterruptionLevel.Critical2,
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
                InterruptionLevel = UNNotificationInterruptionLevel.Critical2,
            };
            var request =
                UNNotificationRequest.FromIdentifier(id, content, trigger);
            await nc.AddNotificationRequestAsync(request);
           // Notifications.Add(not, request);
            NotificationActivated?.Invoke(this, new NotificationActivatedEventArgs(not, id));
        });
    }

    public string? LaunchActionId { get; }= "LLC.Mac";
    public NotificationManagerCapabilities Capabilities { get; private set; }
    public event EventHandler<NotificationActivatedEventArgs>? NotificationActivated;
    public event EventHandler<NotificationDismissedEventArgs>? NotificationDismissed;
    public void Dispose()
    {
        _notificationCenter?.Dispose();
    }
}