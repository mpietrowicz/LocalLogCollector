using System;
using Avalonia;
using Avalonia.ReactiveUI;
using DesktopNotifications;
using Foundation;
using LLC.Infrastructure;

namespace LLC.Mac;

sealed class Program
{


    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .SetupDesktopNotificationsCustom(new MacNotificationManager())
            .UseReactiveUI();
}