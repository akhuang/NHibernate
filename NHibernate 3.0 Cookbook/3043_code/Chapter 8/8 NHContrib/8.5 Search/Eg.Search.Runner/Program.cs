using System.Collections.Generic;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Search;
using System.Linq;

namespace Eg.Search.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      

      XmlConfigurator.Configure();
      var log = LogManager.GetLogger(typeof(Program));

      NHibernateProfiler.Initialize();

      var cfg = new SearchConfiguration();
      var sessionFactory = cfg.BuildSessionFactory();

      var theBook = new Book()
                      {
                        Name = @"Gödel, Escher, Bach: 
An Eternal Golden Braid",
                        Author = "Douglas Hofstadter",
                        Description =
                          @"This groundbreaking Pulitzer 
Prize-winning book sets the standard for 
interdisciplinary writing, exploring the 
patterns and symbols in the thinking of 
mathematician Kurt Godel, artist M.C. Escher, 
and composer Johann Sebastian Bach.",
                        ISBN = "978-0465026562",
                        UnitPrice = 22.95M
                      };

      var theOtherBook = new Book()
                           {
                             Name = "Technical Writing",
                             Author = "Joe Professor",
                             Description = "College text",
                             ISBN = "123-1231231234",
                             UnitPrice = 143.73M
                           };

      var thePoster = new Product()
                        {
                          Name = "Ascending and Descending",
                          Description = "Poster of famous Escher print",
                          UnitPrice = 7.95M
                        };

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          session.Delete("from Product");
          tx.Commit();
        }
      }

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          session.Save(theBook);
          session.Save(theOtherBook);
          session.Save(thePoster);
          tx.Commit();
        }
      }


      var products = GetEscherProducts(sessionFactory);
      OutputProducts(products, log);

      var books = GetEscherBooks(sessionFactory);
      OutputProducts(books.Cast<Product>(), log);
    }

    private static void OutputProducts(
      IEnumerable<Product> products,
      ILog log)
    {

      foreach (var product in products)
      {
        log.InfoFormat("Found {0} with price {1:C}",
                       product.Name, product.UnitPrice);
      }

    }

    private static IEnumerable<Product> GetEscherProducts(
      ISessionFactory sessionFactory)
    {
      IEnumerable<Product> results;
      using (var session = sessionFactory.OpenSession()
                           as IFullTextSession)
      {
        using (var tx = session.BeginTransaction())
        {
          var queryString = "Description:Escher";
          var query = session
            .CreateFullTextQuery<Product>(queryString);
          results = query.List<Product>();
          tx.Commit();
        }
      }
      return results;
    }

    private static IEnumerable<Book> GetEscherBooks(
      ISessionFactory sessionFactory)
    {
      IEnumerable<Book> results;
      using (var session = sessionFactory.OpenSession()
                           as IFullTextSession)
      {
        using (var tx = session.BeginTransaction())
        {
          var queryString = "Description:Escher";
          var query = session
            .CreateFullTextQuery<Book>(queryString);
          results = query.List<Book>();
          tx.Commit();
        }
      }
      return results;

    }
  }
}
