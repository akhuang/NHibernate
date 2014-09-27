using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;

namespace Eg.Shards.Runner
{
  class Program
  {
    static void Main(string[] args)
    {

      NHibernateProfiler.Initialize();

      var connStrNames = new List<string>();
      connStrNames.Add("Shard1");
      connStrNames.Add("Shard2");
      connStrNames.Add("Shard3");

      var shardStrategy = new ShardStrategyFactory();

      var sessionFactory = new ShardConfiguration()
        .GetSessionFactory(connStrNames, shardStrategy);

      ClearDB(sessionFactory);

      var p1 = new Product()
                 {
                   Name = "Water Hose",
                   Description = "50 ft.",
                   UnitPrice = 17.46M
                 };

      var p2 = new Product()
                 {
                   Name = "Water Sprinkler",
                   Description = "Rust resistant plastic",
                   UnitPrice = 4.95M
                 };

      var p3 = new Product()
                 {
                   Name = "Beach Ball",
                   Description = "Hours of fun",
                   UnitPrice = 3.45M
                 };


      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          session.Save(p1);
          session.Save(p2);
          session.Save(p3);
          tx.Commit();
        }
        session.Close();
      }

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var query = 
            "from Product p where upper(p.Name) " +
            "like '%WATER%'";
          var products = session.CreateQuery(query)
            .List();

          foreach (Product p in products)
            Console.WriteLine(p.Name);

          tx.Commit();
        }
        session.Close();
      }

      Console.ReadKey();
    }

    private static void ClearDB(ISessionFactory sessionFactory)
    {
      using (var s = sessionFactory.OpenSession())
      {
        using (var tx = s.BeginTransaction())
        {
          var products = s.CreateQuery("from Product")
            .List();
          foreach (Product product in products)
            s.Delete(product);
          tx.Commit();
        }
        s.Close();
      }
    }

  }
}
