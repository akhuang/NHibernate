using System.Collections.Generic;
using Eg.Core;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace CriteriaExample
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
      var query = session.CreateCriteria<Movie>()
        .Add(Restrictions.Eq("Director", directorName))
        .AddOrder(Order.Asc("UnitPrice"));

      return query.List<Movie>();
   }

    static IList<Movie> GetMoviesWith(
      string actorName,
      ISession session)
    {
      var query = session.CreateCriteria<Movie>()
        .AddOrder(Order.Asc("UnitPrice"))
        .CreateCriteria("Actors", JoinType.InnerJoin)
        .Add(Restrictions.Eq("Actor", actorName));
      return query.List<Movie>();
    }

    static Book GetBookByISBN(
      string ISBN,
      ISession session)
    {
      var query = session.CreateCriteria<Book>()
        .Add(Restrictions.Eq("ISBN", ISBN));
      return query.UniqueResult<Book>();
    }

    static IList<Product> GetProductByPrice(
      decimal minPrice,
      decimal maxPrice,
      ISession session)
    {
      var query = session.CreateCriteria<Product>()
        .Add(Restrictions.And(
          Restrictions.Ge("UnitPrice", minPrice),
          Restrictions.Le("UnitPrice", maxPrice)
        ))
        .AddOrder(Order.Asc("UnitPrice"));

      var query2 = session.CreateCriteria<Product>()
        .Add(Restrictions.Between(
          "UnitPrice", minPrice, maxPrice))
        .AddOrder(Order.Asc("UnitPrice"));

      return query.List<Product>();
    }
  }
}
