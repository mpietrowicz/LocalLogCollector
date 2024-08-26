using Avalonia.Controls.Notifications;
using LLC.Abstraction.Iterfaces;
using Splat;

namespace LLC.Abstraction;

public class MainMainNotifications : IMainNotifications
{
    public EventHandler<DesktopNotifications.Notification> Notify { get; set; }

    private static DesktopNotifications.INotificationManager? _systemManager;

    private static DesktopNotifications.INotificationManager SystemManager => _systemManager ??=
        Locator.Current.GetService<DesktopNotifications.INotificationManager>();


    public void Show(string title, string messge)
    {
        ShowAsync(title, messge).Wait();
    }

    public async Task ShowAsync(string title, string messge)
    {
        var send = SystemManager.ShowNotification(new DesktopNotifications.Notification()
        {
            Title = title,
            Body = messge
        });
        Notify?.Invoke(this, new DesktopNotifications.Notification()
        {
            Title = title,
            Body = messge
        });
        await send;
    }
}