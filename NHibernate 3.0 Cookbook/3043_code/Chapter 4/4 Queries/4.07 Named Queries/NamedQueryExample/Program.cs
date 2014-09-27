using Eg.Core;
using NHibernate.Cfg;

namespace NamedQueryExample
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var isbn = "3043";
          
          var query = session.GetNamedQuery("GetBookByISBN")
            .SetString("isbn", isbn);

          var nhCookbook = query.UniqueResult<Book>();
          
          tx.Commit();
        }
      }

    }
  }
}
