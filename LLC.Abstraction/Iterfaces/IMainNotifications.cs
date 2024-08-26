namespace LLC.Abstraction.Iterfaces;

public interface IMainNotifications
{
   EventHandler<DesktopNotifications.Notification> Notify { get; set; }
   public Task ShowAsync(string title, string messge);
   public void Show(string title, string messge);
}