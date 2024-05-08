using System.Reactive;
using System.Reactive.Disposables;
using LLC.Abstraction.AbstractClasses;
using LLC.Models;
using Material.Icons;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace LLC.ViewModels;


public class MainWindowViewModel : ViewModelBase, IActivatableViewModel
{
    [Reactive]
    public FluentThemeConfig? ThemeConfig { get; set; } = Locator.Current.GetService<FluentThemeConfig>();
    
    public ReactiveCommand<Unit, Unit> ChangeTheme { get; set; }
    public ViewModelActivator Activator { get; } = new();
    [Reactive]
    public string ThemeText { get; set; } = String.Empty;

    [Reactive] public MaterialIconKind ThemeIco { get; set; } = MaterialIconKind.MoonWaningCrescent;
    
    public MainWindowViewModel()
    {
        this.WhenActivated(HandleActivation);
      
    }

    private void HandleActivation(CompositeDisposable obj)
    {
        ThemeText = ThemeConfig.IsDarkMode ?  "Switch to Light Mode" : "Switch to Dark Mode";
        
        ChangeTheme = ReactiveCommand.Create(() =>
        {
            ThemeConfig.IsDarkMode = !ThemeConfig.IsDarkMode;
        }).DisposeWith(obj);
        ChangeTheme.Subscribe(_ =>
        {
            ThemeIco = ThemeConfig.IsDarkMode ? MaterialIconKind.MoonWaningCrescent : MaterialIconKind.WhiteBalanceSunny;
            ThemeText = ThemeConfig.IsDarkMode ?  "Switch to Light Mode" : "Switch to Dark Mode";
        }).DisposeWith(obj);
    }
}