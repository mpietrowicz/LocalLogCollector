using DesktopNotifications;

namespace LLC.Infrastructure;


public class FakeNotificationManager : INotificationManager
{
    public void Dispose()
    {
      
    }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public Task ShowNotification(Notification notification, DateTimeOffset? expirationTime = null)
    {
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