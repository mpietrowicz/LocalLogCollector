using Avalonia;
using Avalonia.Logging;
using Serilog;

namespace LLC.Infrastructure.Sinks;

public static class CustomSinkSerilogAvaloniaExtensions
{
    public static AppBuilder LogToSerilog(this AppBuilder builder,
        ILogger logger,
        LogEventLevel level = LogEventLevel.Warning,
        params string[] areas)
    {
       
        
        Logger.Sink = new CustomSinkSerilogAvalonia(level, areas, logger);
        return builder;
    }
}

public class CustomSinkSerilogAvalonia(LogEventLevel level, string[] areas, ILogger logger) : ILogSink
{
    public LogEventLevel Level { get; } = level;
    public string[] Areas { get; } = areas;
    public ILogger Logger { get; } = logger;

    public bool IsEnabled(LogEventLevel level, string area)
    {
        return level >= Level && (Areas.Length == 0 || Areas.Contains(area));
    }

    public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
    {
        if (!IsEnabled(level, area))
        {
            return;
        }
        Logger.Write(Serilog.Events.LogEventLevel.Verbose, messageTemplate);
    }

    public void Log(LogEventLevel level, string area, object? source, string messageTemplate,
        params object?[] propertyValues)
    {
        if (!IsEnabled(level, area))
        {
            return;
        }
        Logger.Write(Serilog.Events.LogEventLevel.Verbose, messageTemplate);
    }
}