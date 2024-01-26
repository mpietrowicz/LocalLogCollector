using System;
using System.Threading.Tasks;
using DesktopNotifications;
using Foundation;

namespace LLC.Mac;


public class MacNotificationManager : INotificationManager, IDisposable
{
  

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public Task ShowNotification(Notification not, DateTimeOffset? expirationTime = null)
    {
        var notification = new NSUserNotification();

// Add text and sound to the notification
        notification.Title = not.Title;
        notification.InformativeText = not.Body;
        notification.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
        notification.HasActionButton = false;
        NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);
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