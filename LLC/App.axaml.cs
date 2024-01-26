using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
<<<<<<< HEAD
using DesktopNotifications;
using LLC.ViewModels;
using LLC.Views;
using Microsoft.CodeAnalysis;
using Splat;
=======
using LLC.ViewModels;
using LLC.Views;
>>>>>>> 454ea5d0745b222818abec4f547ced99e0a0551c

namespace LLC;

public partial class App : Application
{
<<<<<<< HEAD
    
=======
>>>>>>> 454ea5d0745b222818abec4f547ced99e0a0551c
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
<<<<<<< HEAD
     
=======
>>>>>>> 454ea5d0745b222818abec4f547ced99e0a0551c
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}