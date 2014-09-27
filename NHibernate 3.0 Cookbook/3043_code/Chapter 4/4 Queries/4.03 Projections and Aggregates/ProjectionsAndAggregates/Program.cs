using NHibernate.Cfg;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using Eg.Core;
using System.Collections.Generic;
using System.Linq;

namespace ProjectionsAndAggregates
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
          var priceList = GetMoviePriceList(session);

          var avgMoviePrice = GetAverageMoviePrice(session);

          var directorAvgPrice = GetAvgDirectorPrice(session);

          tx.Commit();
        }
      }

    }

    struct NameAndPrice
    {
      public string Name;
      public decimal Price;
    };

    static IEnumerable<NameAndPrice> 
      GetMoviePriceList(ISession session)
    {
      return session.QueryOver<Movie>()
        .Select(m => m.Name, m => m.UnitPrice)
        .List<object[]>()
        .Select(props =>
          new NameAndPrice()
          {
            Name = (string) props[0],
            Price = (decimal)  props[1]
          });
    }

    static double GetAverageMoviePrice(ISession session)
    {
      var query = session.QueryOver<Movie>()
        .Select(Projections.Avg<Movie>(m => m.UnitPrice));
      return query.SingleOrDefault<double>();
    }

    static IEnumerable<NameAndPrice> 
      GetAvgDirectorPrice(ISession session)
    {
      return session.QueryOver<Movie>()
        .Select(list => list
          .SelectGroup(m => m.Director)
          .SelectAvg(m => m.UnitPrice)
        )
        .List<object[]>()
        .Select(props =>
          new NameAndPrice()
          {
            Name = (string)props[0],
            Price = (decimal)props[1]
          });
    }

  }
}
