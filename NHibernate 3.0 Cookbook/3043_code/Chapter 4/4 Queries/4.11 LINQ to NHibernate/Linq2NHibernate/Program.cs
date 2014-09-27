using System.Linq;
using Eg.Core;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace Linq2NHibernate
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {

          var query = from b in session.Query<Book>()
                      where b.ISBN == "3043"
                      select b;

          var nhCookbook = query.SingleOrDefault();

          tx.Commit();
        }
      }
    }
  }
}
