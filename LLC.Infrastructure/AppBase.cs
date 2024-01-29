using Avalonia;
using ReactiveUI;
using Splat;

namespace LLC.Infrastructure;

public abstract class AppBase : Application
{
    public void Register(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.Register(factory, serviceType,contract);
    }
    public void RegisterSingleton(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.RegisterLazySingleton(factory, serviceType,contract);
    }
    public void ScanAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x=>x.FullName.Contains("LLC"));
        foreach (var assembly in assemblies)
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(assembly);
        }
      
    }
}