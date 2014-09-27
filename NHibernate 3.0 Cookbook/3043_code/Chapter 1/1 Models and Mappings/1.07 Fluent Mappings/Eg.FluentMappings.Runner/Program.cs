using Eg.FluentMappings.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;

namespace Eg.FluentMappings.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(cs => cs.FromConnectionStringWithKey("db"))
                    .ProxyFactoryFactory(typeof (ProxyFactoryFactory)))
        .Mappings(m => m.FluentMappings
                         .AddFromAssemblyOf<ProductMapping>()
                         .ExportTo("."));
    }
  }
}
