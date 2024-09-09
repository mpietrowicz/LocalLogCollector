using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Color = System.Drawing.Color;

namespace LLC.Models;

public class FluentThemeConfig : ReactiveObject
{
    [Reactive]
    public FluentThemeCommon Light { get; set; }
    
    [Reactive]
    public FluentThemeCommon Dark { get; set; }

    [Reactive]
    public bool IsDarkMode { get; set; }

    public FluentThemeConfig()
    {
        IsDarkMode = true;
        Light = new FluentThemeCommon();
        Dark = new FluentThemeCommon();
        
        Light.AccentColor = Colors.Green;
        Light.RegionColor = Colors.White;
        Light.ErrorColor = Colors.Red;
        
        Dark.AccentColor = Colors.DarkBlue;
        Dark.RegionColor = Colors.Black;
        Dark.ErrorColor = Colors.Yellow;
        
    }
}