using DynamicData.Binding;
using LLC.Abstraction.AbstractClasses;
using ReactiveUI.Fody.Helpers;
using LLC.Models;
using ReactiveUI;

namespace LLC.ViewModels;

public class AppViewModel : ViewModelBase
{
    [Reactive]
    public FluentThemeConfig? ThemeConfig { get; set; }

    public AppViewModel()
    {
        ThemeConfig ??= new FluentThemeConfig();
        
        
    }
}