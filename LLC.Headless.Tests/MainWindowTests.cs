using Avalonia.Headless.NUnit;
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
    public void Test1()
    {
        var mainWindow = new MainWindow()
        {
            ViewModel = new MainWindowViewModel()
        };
        mainWindow.Activate();
        mainWindow.Show();
       
       Assert.NotNull(mainWindow);
    }
}