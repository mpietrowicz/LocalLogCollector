using System.Collections.Concurrent;
using LLC.Abstraction;

namespace LLC.Services;

public class CollectorService : IService
{

    public bool IsRunning { get; private set; } = false;
    public async Task Run()
    {
        IsRunning = true;
        do
        {
            await Delay();
        } while (IsRunning);
    }

    private Task Delay()
    {
       return Task.Delay(1000);
    }

    public void Stop()
    {
        IsRunning = false;
    }
}