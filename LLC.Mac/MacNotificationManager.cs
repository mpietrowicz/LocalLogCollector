using DesktopNotifications;
using DesktopNotifications.FreeDesktop;

namespace LLC.Infrastructure;


public class MacNotificationManager : INotificationManager, IDisposable
{
  

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Task Initialize()
    {
        throw new NotImplementedException();
    }

    public Task ShowNotification(Notification notification, DateTimeOffset? expirationTime = null)
    {
        throw new NotImplementedException();
    }

    public Task HideNotification(Notification notification)
    {
        throw new NotImplementedException();
    }

    public Task ScheduleNotification(Notification notification, DateTimeOffset deliveryTime,
        DateTimeOffset? expirationTime = null)
    {
        throw new NotImplementedException();
    }

    public string? LaunchActionId { get; }
    public NotificationManagerCapabilities Capabilities { get; }
    public event EventHandler<NotificationActivatedEventArgs>? NotificationActivated;
    public event EventHandler<NotificationDismissedEventArgs>? NotificationDismissed;
}