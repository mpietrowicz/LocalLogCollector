using Avalonia.Threading;
using LLC.ViewModels;
using LLC.Views;

namespace LLC.Headless.Tests.TestsInfrastructure;

public class HeadlessSession : IDisposable
{
    private MainWindow? _window;
    private MainWindowViewModel? _viewModel;

    public void HeadlessSessionStart(Action<MainWindow, MainWindowViewModel> run)
    {
        try
        {
            _viewModel = new MainWindowViewModel();
            _window = new MainWindow()
            {
                ViewModel = _viewModel
            };
            _window.Show();
            _window.Activate();
            Dispatcher.UIThread.RunJobs();
            run(_window, _viewModel);
        }
        finally
        {
            Dispose();
        }
    }


    public void Dispose()
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            _window?.Close();
        });
        _window = null;
        _viewModel = null;
    }
}