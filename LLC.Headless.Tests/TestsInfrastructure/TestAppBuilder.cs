using Avalonia;
using Avalonia.Headless;
using Avalonia.ReactiveUI;
using LLC.Headless.Tests.TestsInfrastructure;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]
namespace LLC.Headless.Tests.TestsInfrastructure;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UseSkia()
            .UseReactiveUI()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions()
            {
                UseHeadlessDrawing = false
            });
    }
}