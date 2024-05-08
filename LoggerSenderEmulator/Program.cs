using System.Net.Sockets;
using Serilog;
using Serilog.Sinks.Udp.TextFormatters;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Udp("localhost", 7071, AddressFamily.InterNetwork,  new Log4netTextFormatter())
   // .WriteTo.Udp("localhost", 7071, AddressFamily.InterNetwork,  new Log4jTextFormatter())
    .CreateLogger();


var logger = Log.ForContext<Program>();

var random = new Random(100);
do
{
    List<Object> randomTypes = new();
    var randTrueFalse = RandTrueFalse(random);
    if (randTrueFalse)
    {
        randomTypes.Add(new { Name = "John", Age = 25 });
    }
    
    logger.Information("Hello from LoggerSenderEmulator",randomTypes.ToArray());

   await Task.Delay(1000);

} while (true);

bool RandTrueFalse(Random random1)
{
    return random1.Next(0, 1) ==0 ? true : false;
}