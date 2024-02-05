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

public partial class App : AppBase, IViewFor<AppViewModel>
{
 
    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as AppViewModel;
    }

    public AppViewModel? ViewModel { get; set; }
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
        // set the theme
        this.WhenAnyValue(x=> x.ViewModel.ThemeConfig, x=>x.ViewModel.ThemeConfig.IsDarkMode).Subscribe(x =>
        {
            var config = x.Item1;
            var isDarkMode = x.Item2;
            if (config is not null)
            {
                var s = Styles.FirstOrDefault(xs=> xs is FluentTheme);
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
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

   
}