using System;
using Eg.ConfORMMapping.Mappings;
using NHibernate.Cfg;

namespace ConfigWithConfORM
{
  class Program
  {
    static void Main(string[] args)
    {
      var mappingFactory = new MappingFactory();
      var mapping = mappingFactory.CreateMapping();

      var nhConfig = new Configuration().Configure();
      nhConfig.AddDeserializedMapping(mapping, null);

      var sessionFactory = nhConfig.BuildSessionFactory();
      Console.WriteLine("NHibernate configured!");
      Console.ReadKey();

    }
  }
}
