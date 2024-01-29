using System.Drawing;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LLC.Models;

public class FluentThemeCommon : ReactiveObject
{
    [Reactive] 
    public Avalonia.Media.Color AccentColor { get; set; }

    [Reactive] 
    public Avalonia.Media.Color RegionColor { get; set; }

    [Reactive] 
    public Avalonia.Media.Color ErrorColor { get; set; }
}