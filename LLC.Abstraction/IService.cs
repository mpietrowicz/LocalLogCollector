namespace LLC.Abstraction;

public interface IService
{
    public bool IsRunning { get; }
    Task Run();
    void Stop();
}