using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DbScripting
{
  class Program
  {
    static void Main(string[] args)
    {
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var schemaExport = new SchemaExport(nhConfig);
      schemaExport
        .SetOutputFile(@"db.sql")
        .Execute(false, false, false);

    }
  }
}
