using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DbGeneration
{
  class Program
  {

    static void Main(string[] args)
    {

      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var schemaExport = new SchemaExport(nhConfig);
      schemaExport.Create(false, true);

    }

  }

}
