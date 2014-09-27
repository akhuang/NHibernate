using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using Eg.Core;

namespace HQLExample
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

          var goodMovies = GetMoviesDirectedBy(
            "Steven Spielberg",
            session);

          var greatMovies = GetMoviesWith(
            "Morgan Freeman",
            session);

          var nhCookbook = GetBookByISBN(
            "3043",
            session);

          var inexpensiveProducts = GetProductByPrice(
            0M, 15M, session);

          var priceList = GetMoviePriceList(session);

          var avgMoviePrice = GetAverageMoviePrice(session);

          var directorAvgPrice = GetAvgDirectorPrice(
            session);

        }
      }
    }

    static IList<Movie> GetMoviesDirectedBy(
      string directorName,
      ISession session)
    {
      var hql = @"from Movie m 
                  where m.Director = :director
                  order by m.UnitPrice asc";
      var query = session.CreateQuery(hql)
        .SetString("director", directorName);
      return query.List<Movie>();
    }

    static IList<Movie> GetMoviesWith(
      string actorName,
      ISession session)
    {
      var hql = @"select m
                  from Movie m
                  inner join m.Actors as ar
                  where ar.Actor = :actorName
                  order by m.UnitPrice asc";
      var query = session.CreateQuery(hql)
        .SetString("actorName", actorName);
      return query.List<Movie>();
    }

    static Book GetBookByISBN(
      string ISBN,
      ISession session)
    {
      var hql = @"from Book b
                  where b.ISBN = :isbn";
      var query = session.CreateQuery(hql)
        .SetString("isbn", ISBN);
      return query.UniqueResult<Book>();
    }

    static IList<Product> GetProductByPrice(
      decimal minPrice,
      decimal maxPrice,
      ISession session)
    {
      var hql = @"from Product p
                  where p.UnitPrice >= :minPrice
                  and p.UnitPrice <= :maxPrice
                  order by p.UnitPrice asc";

      var hql2 = @"from Product p
                   where p.UnitPrice between
                   :minPrice and :maxPrice
                   order by p.UnitPrice asc";

      var query = session.CreateQuery(hql)
        .SetDecimal("minPrice", minPrice)
        .SetDecimal("maxPrice", maxPrice);
      return query.List<Product>();
    }

    static IEnumerable<NameAndPrice>
      GetMoviePriceList(ISession session)
    {
      var hql = @"select new NameAndPrice(
                  m.Name, m.UnitPrice)
                  from Movie m";
      var query = session.CreateQuery(hql);
      return query.List<NameAndPrice>();

//      var hql = @"select m.Name, m.UnitPrice
//                  from Movie m";
//      var query = session.CreateQuery(hql);
//      return query.List<object[]>()
//        .Select(props =>
//          new NameAndPrice(
//            (string)props[0],
//            (decimal)props[1]));
    }

    static double GetAverageMoviePrice(ISession session)
    {
      var hql = @"select avg(m.UnitPrice)
                  from Movie m";
      var query = session.CreateQuery(hql);
      return query.UniqueResult<double>();
    }

    static IEnumerable<NameAndPrice>
      GetAvgDirectorPrice(ISession session)
    {
      var hql = @"select new NameAndPrice(
                    m.Director, avg(m.UnitPrice)
                  )
                  from Movie m
                  group by m.Director";
      var query = session.CreateQuery(hql);
      return query.List<NameAndPrice>();
    }


  
  }
}
