using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using LLC.Abstraction;
using LLC.Models;
using LLC.Models.Events;

namespace LLC.Services;

public class UdpService : IService
{
    private readonly ServiceConfig _config;
    public int listenPort { get; set; } = 7071;

    public IPAddress IpAddress { get; set; } = IPAddress.Any;

    public bool IsRunning { get; private set; } = false;


    public UdpService(ServiceConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        SerializerLog4J = new System.Xml.Serialization.XmlSerializer(typeof(Log4jEvent));
    }

    public XmlSerializer SerializerLog4J { get; set; }

    public async Task Run()
    {
        if (_config.ContainsKey("listenPort"))
            listenPort = int.Parse(_config["listenPort"]);
        if (_config.ContainsKey("ipAddress"))
            IpAddress = IPAddress.Parse(_config["ipAddress"]);

        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
        try
        {
            IsRunning = true;
            while (IsRunning == true)
            {
                byte[] bytes = listener.Receive(ref groupEP);
                //var response = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                //Debug.WriteLine(response);
              //  var serialized = SerializerLog4J.Deserialize(new StringReader(response));
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            IsRunning = false;
            listener.Close();
        }
    }


    public void Stop()
    {
        IsRunning = false;
    }
}