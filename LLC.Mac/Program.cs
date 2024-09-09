using System;
using System.Threading.Tasks;
using AppKit;
using Avalonia;
using Avalonia.Logging;
using Avalonia.ReactiveUI;
using LLC.Infrastructure;
using LLC.Infrastructure.Sinks;
using Serilog;

namespace LLC.Mac;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.File("logs/llc.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("AppStart");
        try
        {
            NSApplication.Init();
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Something went wrong");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
           // .LogToDebug()   
            .LogToSerilog(Log.Logger,LogEventLevel.Verbose)
            .SetupDesktopNotificationsCustom(new MacNotificationManager())
            .UseReactiveUI()
            .UseReactiveUI().With(new AvaloniaNativePlatformOptions()
            {
                AvaloniaNativeLibraryPath = "libAvaloniaNative.dylib"
            });
}