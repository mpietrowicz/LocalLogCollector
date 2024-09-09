using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using FluentAssertions;
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
            frame.Should().NotBeNull();
            frame.Save("file.png");
            // open menu and click on settings and change theme
            window.ClickObject(window.SettingsMenu).Should().BeTrue();
            window.CaptureRenderedFrameAndSave<MainWindow>( $"{this.GetType().Name}/Theme_ChangeTheme_Success/ClickSettingsMenu.png");
            var header = window.SettingsChangeThemeButton.Header as string;
            header.Should().NotBeNull();
            window.ClickObject(window.SettingsChangeThemeButton).Should().BeTrue();
            
            // open settings menu to chek if name is changed 
            window.ClickObject(window.SettingsMenu).Should().BeTrue();
            window.CaptureRenderedFrameAndSave<MainWindow>( $"{this.GetType().Name}/Theme_ChangeTheme_Success/ClickSettingsMenu2.png");

            Assert.That(window.SettingsChangeThemeButton.Header, Is.Not.EqualTo(header));

            window.Should().NotBeNull();
            viewModel.Should().NotBeNull();
        });
    }
}