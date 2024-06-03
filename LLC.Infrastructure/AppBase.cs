using System.Reflection;
using Avalonia;
using LLC.Abstraction;
using ReactiveUI;
using Splat;

namespace LLC.Infrastructure;

public abstract class AppBase : Application
{
    public void Register(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.Register(factory, serviceType, contract);
    }

    protected void RegisterSingleton(Func<object?> factory, Type? serviceType, string? contract = null)
    {
        Locator.CurrentMutable.RegisterLazySingleton(factory, serviceType, contract);
    }

    protected void ScanAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !string.IsNullOrEmpty(x.FullName) && x.FullName.StartsWith("LLC."));
        List<Task> servicesTasks = new();
        servicesTasks.Add(RegisterViewsForViewModels(assemblies));
        
        foreach (var assembly in assemblies)
        {
            var services = assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IService)));
            foreach (var service in services)
            {
                RegisterSingleton(() => Activator.CreateInstance(service), service);
            }
        }
        
        Task.WaitAll(servicesTasks.ToArray());
    }


    private Task RegisterViewsForViewModels(IEnumerable<Assembly> assemblies)
    {
        return Task.Run(() =>
        {
            foreach (var assembly in assemblies)
            {
                Locator.CurrentMutable.RegisterViewsForViewModels(assembly);
            }
        });
    }
}