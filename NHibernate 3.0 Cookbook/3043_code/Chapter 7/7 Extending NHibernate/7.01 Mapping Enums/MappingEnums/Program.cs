using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace MappingEnums
{
  class Program
  {
    static void Main(string[] args)
    {

      log4net.Config.XmlConfigurator.Configure();
      var config = new Configuration().Configure();
      var sessionFactory = config.BuildSessionFactory();

      var log = log4net.LogManager.GetLogger(typeof(Program));

      var se = new SchemaExport(config);
      se.Create(s => log.Info(s), false);

    }
  }
}
