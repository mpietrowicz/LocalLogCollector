using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopNotifications;
using Foundation;

namespace LLC.Mac;

public class MacNotificationManager : INotificationManager
{
    
    private Dictionary<Notification, NSUserNotification> Notifications { get; set; }
    

    public void Dispose()
    {
        foreach (var nsUserNotification in Notifications)
        {
            nsUserNotification.Value.Dispose();
        }
        Notifications.Clear();
        
    }

    public Task Initialize()
    {
        Notifications = new Dictionary<Notification, NSUserNotification>();
        return Task.CompletedTask;
    }

    public Task ShowNotification(Notification not, DateTimeOffset? expirationTime = null)
    {
        var notification = new NSUserNotification(); 
        notification.Title = not.Title;
        notification.InformativeText = not.Body;
        notification.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
        notification.HasActionButton = false;
        NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);
        Notifications.Add(not, notification);
        return Task.CompletedTask;
    }

    public Task HideNotification(Notification notification)
    {
        NSUserNotificationCenter.DefaultUserNotificationCenter
            .RemoveDeliveredNotification(Notifications[notification]);
        return Task.CompletedTask;
    }

    public Task ScheduleNotification(Notification not, DateTimeOffset deliveryTime,
        DateTimeOffset? expirationTime = null)
    {
       
        var notification = new NSUserNotification();
        notification.Title = not.Title;
        notification.InformativeText = not.Body;
        notification.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
        notification.HasActionButton = false;
        notification.DeliveryDate = (NSDate) deliveryTime.DateTime;
        NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);
        Notifications.Add(not, notification);
        return Task.CompletedTask;
    }

    public string? LaunchActionId { get; }
    public NotificationManagerCapabilities Capabilities { get; }
    public event EventHandler<NotificationActivatedEventArgs>? NotificationActivated;
    public event EventHandler<NotificationDismissedEventArgs>? NotificationDismissed;
}