using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DesktopNotifications;
using LLC.ViewModels;
using ReactiveUI;
using Splat;


namespace LLC.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {

        
        InitializeComponent();
        var nm = Locator.Current.GetService<INotificationManager>();
        nm?.ShowNotification(new Notification()
        {
               Title = "Start",
                Body = "AppStart"
        });
       
        this.WhenActivated(HandleActivation);
    }

    private void HandleActivation(CompositeDisposable obj)
    {
        this.BindCommand(ViewModel, x => x.ChangeTheme, x => x.ChangeThemeButton)
            .DisposeWith(obj);
        this.OneWayBind(ViewModel, x => x.ThemeText, x => x.ChangeThemeButton.Header)
            .DisposeWith(obj);
    }
}