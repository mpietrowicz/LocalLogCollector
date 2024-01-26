using Avalonia.Controls;
<<<<<<< HEAD
using DesktopNotifications;
using Splat;
=======
>>>>>>> 454ea5d0745b222818abec4f547ced99e0a0551c

namespace LLC.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
<<<<<<< HEAD
        
        InitializeComponent();
        var nm = Locator.Current.GetService<INotificationManager>();
        nm?.ShowNotification(new Notification()
        {
               Title = "Start",
                Body = "AppStart"
        });
=======
        InitializeComponent();
>>>>>>> 454ea5d0745b222818abec4f547ced99e0a0551c
    }
}