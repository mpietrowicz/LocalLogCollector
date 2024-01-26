using Avalonia.Controls;

using DesktopNotifications;
using Splat;


namespace LLC.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {

        
        InitializeComponent();
        var nm = Locator.Current.GetService<INotificationManager>();
        nm?.ShowNotification(new Notification()
        {
               Title = "Start",
                Body = "AppStart"
        });

    }
}