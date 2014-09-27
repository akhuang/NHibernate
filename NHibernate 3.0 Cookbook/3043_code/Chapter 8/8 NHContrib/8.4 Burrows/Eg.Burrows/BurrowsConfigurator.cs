using HibernatingRhinos.Profiler.Appender.NHibernate;
using log4net.Config;
using NHibernate.Burrow;
using NHibernate.Burrow.Configuration;
using NHibernate.Cfg;

namespace Eg.Burrows
{
  public class BurrowsConfigurator : IConfigurator
  {

    public void Config(IPersistenceUnitCfg puCfg, 
      Configuration nhCfg)
    {
      nhCfg.Configure();
    }

    public void Config(IBurrowConfig val)
    {
      XmlConfigurator.Configure();
      NHibernateProfiler.Initialize();
      var unit = new PersistenceUnitElement
                   {
                     Name = "persistenceUnit1",
                     NHConfigFile = null
                   };
      val.PersistenceUnitCfgs.Add(unit);
    }

  }
}
