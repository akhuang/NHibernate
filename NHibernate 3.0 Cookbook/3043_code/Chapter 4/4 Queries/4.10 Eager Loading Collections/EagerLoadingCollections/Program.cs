using System;
using Eg.Core;
using NHibernate.Cfg;

namespace EagerLoadingCollections
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

          var products = session.CreateQuery(
            "from Product p")
            .Future<Product>();

          var actorRoles = session.CreateQuery(
            @"from Movie m
              left join fetch m.Actors")
            .Future<Movie>();

          foreach (var product in products)
            if (product is Movie)
            {
              var movie = (Movie)product;
              if (movie.Actors.Count > 0)
              {
                Console.WriteLine("{0} starring {1}",
                  movie.Name, movie.Actors[0].Actor);
              }
              else
              {
                Console.WriteLine("{0} starring nobody.",
                  movie.Name);
              }
            }
            else
            {
              Console.WriteLine(product.Name);
            }

          Console.ReadKey();

          tx.Commit();
        }
      }
    }
  }
}
