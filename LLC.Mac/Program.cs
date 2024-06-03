using System;
using System.Threading.Tasks;
using AppKit;
using Avalonia;
using Avalonia.ReactiveUI;
using LLC.Infrastructure;

namespace LLC.Mac;

sealed class Program
{


    [STAThread]
    public static void Main(string[] args)
    {
        NSApplication.Init();
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
            .UseReactiveUI()
    .UseReactiveUI().With(new AvaloniaNativePlatformOptions()
    {
        AvaloniaNativeLibraryPath = "libAvaloniaNative.dylib"
    });
}