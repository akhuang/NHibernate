using System.Collections.Generic;
using Eg.Core;
using NHibernate;
using NHibernate.Cfg;

namespace QueryOverExample
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      //CreateData(sessionFactory);

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

          tx.Commit();
        }
      }

    }

    #region Create Data
    static void CreateData(ISessionFactory sessionFactory)
    {
      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          session.Save(CreateNewMovie());
          session.Save(CreateNewBook());
          tx.Commit();
        }
      }
    }

    static Movie CreateNewMovie()
    {
      return new Movie()
      {
        Name = "Hackers",
        Description = "Bad",
        UnitPrice = 12.59M,
        Director = "Iain Softley",
        Actors = new List<ActorRole>()
        {
          new ActorRole() 
          { 
            Actor = "Jonny Lee Miller", 
            Role="Zero Cool"
          },
          new ActorRole() 
          { 
            Actor = "Angelina Jolie", 
            Role="Acid Burn"
          }
        }
      };
    }

    static Book CreateNewBook()
    {
      return new Book()
      {
        Name = "NHibernate Cookbook 3.0",
        Description = "NHibernate examples",
        UnitPrice = 50M,
        Author = "Jason Dentler",
        ISBN = "3043"
      };
    }
    #endregion

    static IList<Movie> GetMoviesDirectedBy(
      string directorName,
      ISession session)
    {
      var query = session.QueryOver<Movie>()
        .Where(m => m.Director == directorName)
        .OrderBy(m => m.UnitPrice).Asc;
      return query.List();
    }

    static IList<Movie> GetMoviesWith(
      string actorName,
      ISession session)
    {
      var query = session.QueryOver<Movie>()
        .OrderBy(m => m.UnitPrice).Asc
        .Inner.JoinQueryOver<ActorRole>(m => m.Actors)
          .Where(a => a.Actor == actorName);

      return query.List<Movie>();
    }

    static Book GetBookByISBN(
      string ISBN,
      ISession session)
    {
      var query = session.QueryOver<Book>()
        .Where(b => b.ISBN == ISBN);
      return query.SingleOrDefault();
    }

    static IList<Product> GetProductByPrice(
      decimal minPrice,
      decimal maxPrice,
      ISession session)
    {
      /*
      var query = session.QueryOver<Product>()
        .Where(p => p.UnitPrice >= minPrice)
        .And(p => p.UnitPrice <= maxPrice)
        .OrderBy(p => p.UnitPrice).Asc;

      */
      var query = session.QueryOver<Product>()
        .Where(p => p.UnitPrice >= minPrice
          && p.UnitPrice <= maxPrice)
        .OrderBy(p => p.UnitPrice).Asc;

      /*
      var query = session.QueryOver<Product>()
        .WhereRestrictionOn(p => p.UnitPrice)
        .IsBetween(minPrice).And(maxPrice)
        .OrderBy(p => p.UnitPrice).Asc;
      */

      return query.List<Product>();
    }


  }
}
