using Avalonia;
using Avalonia.Headless;
using Avalonia.ReactiveUI;
using DesktopNotifications;
using LLC.Headless.Tests.TestsInfrastructure;
using LLC.Infrastructure;
using Moq;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]
namespace LLC.Headless.Tests.TestsInfrastructure;


public class TestAppBuilder
{
    private static Mock<INotificationManager> MockNotificationManager;

    public static AppBuilder BuildAvaloniaApp()
    {
        MockNotificationManager = new Mock<INotificationManager>();

        MockNotificationManager.Setup(x => x.Initialize()).Returns(Task.CompletedTask);
        MockNotificationManager.Setup(x => x.ShowNotification(It.IsAny<Notification>(), null)).Returns(Task.CompletedTask);
        MockNotificationManager
            .Setup(x => x.ShowNotification(It.IsAny<Notification>(), It.IsAny<DateTimeOffset>())).Returns(Task.CompletedTask);
        MockNotificationManager.Setup(x => x.Dispose());
        MockNotificationManager.Setup(x => x.Capabilities).Returns(NotificationManagerCapabilities.BodyText |
                                                                NotificationManagerCapabilities.Audio |
                                                                NotificationManagerCapabilities.Icon);
        MockNotificationManager.Setup(x => x.LaunchActionId).Returns("launch");
        
        return AppBuilder.Configure<App>()
            .UseSkia()
            .UseReactiveUI()
            .SetupDesktopNotificationsCustom(MockNotificationManager.Object)
            .UseHeadless(new AvaloniaHeadlessPlatformOptions()
            {
                UseHeadlessDrawing = false
            });
    }
}