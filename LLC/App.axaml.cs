using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using DesktopNotifications;
using LLC.Abstraction;
using LLC.Infrastructure;
using LLC.Models;
using LLC.Services;
using LLC.ViewModels;
using LLC.Views;
using Splat;
using ReactiveUI;

namespace LLC;

public partial class App : AppBase, IViewFor<AppViewModel>
{

    private List<Task> ServicesTasks { get; set; } = new();
    private List<IService> Services { get; set; } = new();

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as AppViewModel;
    }

    public AppViewModel? ViewModel { get; set; }

    public override void Initialize()
    {
        Locator.CurrentMutable.RegisterLazySingleton(() => new ConventionalViewLocator(), typeof(IViewLocator));
        RegisterSingleton(() => new FluentThemeConfig(), typeof(FluentThemeConfig));
        RegisterSingleton(() => new AppViewModel()
        {
            ThemeConfig = Locator.Current.GetService<FluentThemeConfig>()
        }, typeof(AppViewModel));
        RegisterSingleton(() => new MainWindow(), typeof(MainWindow));
        RegisterSingleton(() => new MainWindowViewModel(), typeof(MainWindowViewModel));

        ScanAssemblies();
        AvaloniaXamlLoader.Load(this);

        ViewModel = Locator.Current.GetService<AppViewModel>() ?? throw new ArgumentNullException(nameof(AppViewModel));
        DataContext = ViewModel;
        
       
        
        // set the theme
        this.WhenAnyValue(x => x.ViewModel.ThemeConfig, x => x.ViewModel.ThemeConfig.IsDarkMode).Subscribe(x =>
        {
            var config = x.Item1;
            var isDarkMode = x.Item2;
            if (config is not null)
            {
                var s = Styles.FirstOrDefault(xs => xs is FluentTheme);
                if (s != null) Styles.Remove(s);
                var theme = new FluentTheme()
                {
                    Palettes =
                    {
                        [ThemeVariant.Light] = new ColorPaletteResources()
                        {
                            Accent = config.Light.AccentColor,
                            RegionColor = config.Light.RegionColor,
                            ErrorText = config.Light.ErrorColor
                        },
                        [ThemeVariant.Dark] = new ColorPaletteResources()
                        {
                            Accent = config.Dark.AccentColor,
                            RegionColor = config.Dark.RegionColor,
                            ErrorText = config.Dark.ErrorColor
                        },
                    }
                };
                Styles.Add(theme);
                RequestedThemeVariant = isDarkMode ? ThemeVariant.Dark : ThemeVariant.Light;
              
            }
        });
    }


    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = Locator.Current.GetService<MainWindow>() ??
                         throw new ArgumentNullException(nameof(MainWindow));
            window.DataContext = Locator.Current.GetService<MainWindowViewModel>();
            desktop.MainWindow = window;
            desktop.Exit += OnExit;
        }

        var service = new UdpService(new ServiceConfig());


        base.OnFrameworkInitializationCompleted();
        Task.Run(() =>
        {
            ServicesTasks.Add(service.Run());
            Services.Add(service);
        });
      
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        foreach (var service in Services)
        {
            service.Stop();
        }

        foreach (var service in ServicesTasks)
        {
            service.Dispose();
        }
    }
}