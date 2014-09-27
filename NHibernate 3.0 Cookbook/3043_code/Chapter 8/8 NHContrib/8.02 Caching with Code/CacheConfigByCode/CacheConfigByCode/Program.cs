using System;
using Eg.Core;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using Environment = NHibernate.Cfg.Environment;

namespace CacheConfigByCode
{
  class Program
  {
    static void Main(string[] args)
    {
      var nhConfig = new Configuration().Configure();
      ConfigureCaching(nhConfig);
      var sessionFactory = nhConfig.BuildSessionFactory();
      Console.WriteLine("NHibernate cache configured!");
      Console.ReadKey();
    }

    static void ConfigureCaching(Configuration nhConfig)
    {
    nhConfig
      .SetProperty(Environment.UseSecondLevelCache, "true")
      .SetProperty(Environment.UseQueryCache, "true")
      .Cache(c => c.Provider<SysCacheProvider>())
      .EntityCache<Product>(c =>
          {
            c.Strategy = EntityCacheUsage.Readonly;
            c.RegionName = "hourly";
          })
      .EntityCache<ActorRole>(c =>
          {
            c.Strategy = EntityCacheUsage.Readonly;
            c.RegionName = "hourly";
          })
      .EntityCache<Movie>(c => c.Collection(
          movie => movie.Actors,
          coll =>
          {
            coll.Strategy = EntityCacheUsage.Readonly;
            coll.RegionName = "hourly";
          }));
     
    }
  }
}