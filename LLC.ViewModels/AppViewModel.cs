using System.Reactive;
using DynamicData.Binding;
using LLC.Abstraction.AbstractClasses;
using ReactiveUI.Fody.Helpers;
using LLC.Models;
using ReactiveUI;
using Splat;

namespace LLC.ViewModels;

public class AppViewModel : ViewModelBase
{
    [Reactive]
    public FluentThemeConfig? ThemeConfig { get; set; } = Locator.Current.GetService<FluentThemeConfig>();
 

    
    public AppViewModel()
    {
    

        
    }

    
}