using System.Xml;

namespace FinSight.Usuario.Infrastructure.Utilidades
{
    public class Constants
    {
        public static string? Connection { get; set; }

        public string ConfigPath
        {
            set
            {
                XmlDocument xml = new();
                xml.Load(value);
                XmlNode? node = xml.DocumentElement?.SelectSingleNode("connectionStrings/add[@name='FINSIGHT']");
                Connection = node?.Attributes?["value"]?.Value;
            }
        }
    }
}
