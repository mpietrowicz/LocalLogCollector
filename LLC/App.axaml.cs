using System;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using LLC.Infrastructure;
using LLC.Models;
using LLC.ViewModels;
using LLC.Views;
using Splat;
using ReactiveUI;

namespace LLC;

public partial class App : AppBase
{
    public AppViewModel ViewModel { get; set; }

    public override void Initialize()
    {
        RegisterSingleton(() => new FluentThemeConfig(), typeof(FluentThemeConfig));
        RegisterSingleton(() => new AppViewModel()
        {
            ThemeConfig = Locator.Current.GetService<FluentThemeConfig>()
        }, typeof(AppViewModel));
        ScanAssemblies();
        AvaloniaXamlLoader.Load(this);
        
        ViewModel = Locator.Current.GetService<AppViewModel>() ?? throw new ArgumentNullException(nameof(AppViewModel));
        DataContext = ViewModel;
        this.WhenAnyValue(x=> x.ViewModel.ThemeConfig).Subscribe(x =>
        {
            if (x is not null)
            {
                var s = Styles.FirstOrDefault(xs=> xs is FluentTheme);
                if (s != null) Styles.Remove(s);
                var theme = new FluentTheme()
                {
                    Palettes = 
                    {
                        [ThemeVariant.Light] = new ColorPaletteResources()
                        {
                             Accent = x.Light.AccentColor,
                             RegionColor = x.Light.RegionColor,
                             ErrorText = x.Light.ErrorColor
                        },
                        [ThemeVariant.Dark] = new ColorPaletteResources()
                        {
                            Accent = x.Dark.AccentColor,
                            RegionColor = x.Dark.RegionColor,
                            ErrorText = x.Dark.ErrorColor
                        },
                    }
                };
                Styles.Add(theme);
                RequestedThemeVariant = ViewModel.ThemeConfig.IsDarkMode ? ThemeVariant.Dark : ThemeVariant.Light;
            }
        });
     
    }


    public override void OnFrameworkInitializationCompleted()
    {

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