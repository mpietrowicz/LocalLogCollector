using System;
using Avalonia;
using Avalonia.ReactiveUI;
using LLC.Infrastructure;

namespace LLC.Viewer;

sealed class Program
{
   
    // only for debugging views
    [STAThread]
    public static void Main(string[] args)
    {
#if DEBUG
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
#endif
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}