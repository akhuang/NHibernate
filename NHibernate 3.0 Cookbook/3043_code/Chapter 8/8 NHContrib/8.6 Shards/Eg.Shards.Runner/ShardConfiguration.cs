using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Shards;
using NHibernate.Shards.Cfg;
using NHibernate.Shards.Session;
using NHibernate.Shards.Strategy;

namespace Eg.Shards.Runner
{
  public class ShardConfiguration
  {

    private Configuration GetConfiguration(
      string connStrName, 
      int shardId)
    {
      var cfg = new Configuration()
        .SessionFactoryName("SessionFactory" 
            + shardId.ToString())
        .Proxy(p => 
          p.ProxyFactoryFactory<ProxyFactoryFactory>())
        .DataBaseIntegration(db =>
             {
               db.Dialect<MsSql2008Dialect>();
               db.ConnectionStringName = connStrName;
             })
        .AddAssembly("Eg.Shards")
        .SetProperty(
          ShardedEnvironment.ShardIdProperty,
          shardId.ToString());
      return cfg;
    }

    private IShardConfiguration GetShardCfg(
      string connStrName,
      int shardId)
    {
      var cfg = GetConfiguration(connStrName, shardId);
      return new ConfigurationToShardConfigurationAdapter(
        cfg);
    }

    private IList<IShardConfiguration> GetShardCfg(
      IEnumerable<string> connStrNames)
    {
      var cfg = new List<IShardConfiguration>();
      int shardId = 1;
      foreach (var connStrName in connStrNames)
        cfg.Add(GetShardCfg(connStrName, shardId++));
      return cfg;
    }

    public IShardedSessionFactory GetSessionFactory(
      IEnumerable<string> connStrNames,
      IShardStrategyFactory shardStrategyFactory)
    {
      var prototypeCfg = GetConfiguration(
        connStrNames.First(), 1);

      var cfg = new ShardedConfiguration(
        prototypeCfg,
        GetShardCfg(connStrNames),
        shardStrategyFactory);

      return cfg.BuildShardedSessionFactory();
    }


  }
}
