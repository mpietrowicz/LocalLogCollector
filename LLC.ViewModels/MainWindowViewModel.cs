using LLC.Abstraction.AbstractClasses;
using LLC.Models;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace LLC.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive]
    public FluentThemeConfig? ThemeConfig { get; set; } = Locator.Current.GetService<FluentThemeConfig>();
}