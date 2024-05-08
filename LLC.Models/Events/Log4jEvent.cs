using System.Xml.Serialization;

namespace LLC.Models.Events;


[XmlRoot(ElementName="event")]
public class Log4jEvent { 

    [XmlElement(ElementName="message")] 
    public string Message { get; set; } 

    [XmlAttribute(AttributeName="log4j")] 
    public string Log4j { get; set; } 

    [XmlAttribute(AttributeName="level")] 
    public string Level { get; set; } 

    [XmlAttribute(AttributeName="logger")] 
    public string Logger { get; set; } 

    [XmlAttribute(AttributeName="timestamp")] 
    public double Timestamp { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}