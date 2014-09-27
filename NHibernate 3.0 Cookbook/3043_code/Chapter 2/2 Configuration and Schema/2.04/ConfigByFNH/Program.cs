using System;
using Eg.FluentMappings.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace ConfigByFNH
{
  class Program
  {
    static void Main(string[] args)
    {
      var nhConfig = Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2008
          .ConnectionString(connstr =>
            connstr.FromConnectionStringWithKey("db")
          )
          .ProxyFactoryFactory<ProxyFactoryFactory>()
          .AdoNetBatchSize(100)
        )
        .Mappings(mappings => mappings.FluentMappings
          .AddFromAssemblyOf<ProductMapping>()
        )
        .BuildConfiguration();
      var sessionFactory = nhConfig.BuildSessionFactory();
      Console.WriteLine("NHibernate configured fluently!");
      Console.ReadKey();
    }
  }
}

