using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input;
using Avalonia.Threading;

namespace LLC.Headless.Tests.Extensions;

public static class HeadlessExtensions
{
    public static void CaptureRenderedFrameAndSave<TType>(this TopLevel topLevel, string saveName, params object[]? args)
        where TType : class
    {
        Dispatcher.UIThread.RunJobs();
        AvaloniaHeadlessPlatform.ForceRenderTimerTick();
        var frame= topLevel.GetLastRenderedFrame();
        var frameDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Frames", typeof(TType).Name);
        if(!Directory.Exists(frameDirectory))
            Directory.CreateDirectory(frameDirectory);
        var framePath = Path.Combine(frameDirectory,
            saveName.Replace(".png", $"_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png"));
        var infor = new FileInfo(framePath);
        if(!infor.Directory.Exists)
            Directory.CreateDirectory(infor.Directory.FullName);
        
        frame?.Save(string.Format(framePath, args ?? Array.Empty<Object>()));
        Dispatcher.UIThread.RunJobs();
        Console.WriteLine($"Saved frame to {string.Format(framePath, args ?? Array.Empty<Object>())}");
    }
    public static bool ClickObject(this Window window, Control controlToClickOn)
    {
        if (controlToClickOn is { IsVisible: true, IsEnabled: true })
        {
            controlToClickOn.Focus();
            window?.KeyPressQwerty(PhysicalKey.Enter, RawInputModifiers.None);
            Dispatcher.UIThread.RunJobs();
            return true;
        }

        return false;


    }
}