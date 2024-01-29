using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using DesktopNotifications.FreeDesktop;
using DesktopNotifications.Windows;
using System;
using DesktopNotifications;
using Splat;

namespace LLC.Infrastructure;

/// <summary>
/// Extensions for <see cref="AppBuilder" />
/// </summary>
public static class AppBuilderExtensions
{
    
    
    public static AppBuilder SetupDesktopNotificationsCustom(this AppBuilder builder, INotificationManager manager)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));
        if (manager == null) throw new ArgumentNullException(nameof(manager));
        manager.Initialize().GetAwaiter().GetResult();

        var manager_ = manager;
        builder.AfterSetup(b =>
        {
            if (b.Instance?.ApplicationLifetime is IControlledApplicationLifetime lifetime)
            {
                lifetime.Exit += (s, e) => { manager_.Dispose(); };
            }
        });
        Locator.CurrentMutable.RegisterConstant(manager, typeof(INotificationManager));
        return builder;
    }
    /// <summary>
    /// Setups the <see cref="INotificationManager" /> for the current platform and
    /// binds it to the service locator (<see cref="AvaloniaLocator" />).
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static AppBuilder SetupDesktopNotifications(this AppBuilder builder)
    {
        if (Design.IsDesignMode)
        {
            Locator.CurrentMutable.RegisterConstant(new FakeNotificationManager(), typeof(INotificationManager));
            return builder;
        }
     
        
        INotificationManager? manager;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            var context = WindowsApplicationContext.FromCurrentProcess();
            manager = new WindowsNotificationManager(context);
        }
        else if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            var context = FreeDesktopApplicationContext.FromCurrentProcess();
            manager = new FreeDesktopNotificationManager(context);
        }
        else
        {
            throw new Exception($"Unsupported platform {Environment.OSVersion.Platform.ToString()}");
        }

        //TODO Any better way of doing this?
        manager.Initialize().GetAwaiter().GetResult();

        var manager_ = manager;
        builder.AfterSetup(b =>
        {
            if (b.Instance?.ApplicationLifetime is IControlledApplicationLifetime lifetime)
            {
                lifetime.Exit += (s, e) => { manager_.Dispose(); };
            }
        });
        Locator.CurrentMutable.RegisterConstant(manager, typeof(INotificationManager));
        return builder;
    }
}