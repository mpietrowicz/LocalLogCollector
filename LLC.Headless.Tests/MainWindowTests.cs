using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using LLC.Headless.Tests.Extensions;
using LLC.Headless.Tests.TestsInfrastructure;
using LLC.ViewModels;
using LLC.Views;
using Splat;

namespace LLC.Headless.Tests;

public class MainWindowTests
{
    [SetUp]
    public void Setup()
    {
    }

    [AvaloniaTest]
    public void Theme_ChangeTheme_Success()
    {
        using var session = new HeadlessSession();
        session.HeadlessSessionStart((window, viewModel) =>
        {
            var frame = window.CaptureRenderedFrame();
            Assert.NotNull(frame);
            frame.Save("file.png");
            // open menu and click on settings and change theme
            Assert.True(window.ClickObject(window.SettingsMenu));
            window.CaptureRenderedFrameAndSave<MainWindow>( $"{this.GetType().Name}/Theme_ChangeTheme_Success/ClickSettingsMenu.png");
            var header = window.SettingsChangeThemeButton.Header as string;
            Assert.NotNull(header);
            Assert.True(window.ClickObject(window.SettingsChangeThemeButton));
            
            // open settings menu to chek if name is changed 
            Assert.True(window.ClickObject(window.SettingsMenu));
            window.CaptureRenderedFrameAndSave<MainWindow>( $"{this.GetType().Name}/Theme_ChangeTheme_Success/ClickSettingsMenu2.png");

            Assert.That(window.SettingsChangeThemeButton.Header, Is.Not.EqualTo(header));

            Assert.NotNull(window);
            Assert.NotNull(viewModel);
        });
    }
}