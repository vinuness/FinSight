using System.Xml;

namespace FinSight.Metas.Infrastructure.Utilidades
{
    public class Constants
    {
        public static string? Connection { get; set; }

        public static string ConfigPath
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
