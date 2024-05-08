using Avalonia;
using ReactiveUI;
using Splat;

namespace LLC.Infrastructure;

public abstract class AppBase : Application
{
  
    public void Register(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.Register(factory, serviceType, contract);
    }
    public void RegisterSingleton(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.RegisterLazySingleton(factory, serviceType, contract);
    }
    public void ScanAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x =>
        {
            if (!string.IsNullOrEmpty(x.FullName))
            {
                return x.FullName.Contains("LLC");

            }
            return false;
        });
        foreach (var assembly in assemblies)
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(assembly);
        }

    }
}