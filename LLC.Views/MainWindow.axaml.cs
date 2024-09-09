using System.Reactive;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Notifications;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using LLC.ViewModels;
using ReactiveUI;
using Notification = Avalonia.Controls.Notifications.Notification;

[assembly: InternalsVisibleTo("LLC.Headless.Tests")]

namespace LLC.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    WindowNotificationManager? _notificationManager;

    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(HandleActivation);
    }

    private void HandleActivation(CompositeDisposable obj)
    {
        _notificationManager ??= new WindowNotificationManager(this)
        {
            Position = NotificationPosition.TopRight,
            MaxItems = 3,
        };

        this.BindCommand(ViewModel, x => x.ChangeTheme, x => x.SettingsChangeThemeButton)
            .DisposeWith(obj);
        this.OneWayBind(ViewModel, x => x.ThemeText, x => x.SettingsChangeThemeButton.Header)
            .DisposeWith(obj);
        this.BindInteraction(ViewModel, x => x.ShowNotification, async x =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    _notificationManager?.Show(new Notification(x.Input.Title, x.Input.Body));
                });
                x.SetOutput(Unit.Default);
            })
            .DisposeWith(obj);
    }
}