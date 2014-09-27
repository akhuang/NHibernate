using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Eg.ConfORMMapping.Mappings;
using NHibernate.Cfg.MappingSchema;

namespace Eg.ConfORMMapping.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var modelMapper = new MappingFactory();
      var mapping = modelMapper.CreateMapping();
      Debug.WriteLine(Serialize(mapping));
    }

    public static string Serialize(HbmMapping hbmMapping)
    {
      var setting = new XmlWriterSettings { Indent = true };
      var serializer = new XmlSerializer(typeof(HbmMapping));
      using (var memStream = new MemoryStream(2048))
      using (var xmlWriter = XmlWriter.Create(memStream, setting))
      {
        serializer.Serialize(xmlWriter, hbmMapping);
        memStream.Flush();
        memStream.Position = 0;
        using (var sr = new StreamReader(memStream))
        {
          return sr.ReadToEnd();
        }
      }
    }


  }
}
