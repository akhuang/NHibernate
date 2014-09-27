using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using Eg.Core;

namespace StatelessSession
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      using (var session = sessionFactory.OpenStatelessSession())
      {
        using (var tx = session.BeginTransaction())
        {
          for (int i = 0; i < 1000; i++)
            session.Insert(new Movie()
            {
              Name = "Movie " + i.ToString(),
              Description = "A great movie!",
              UnitPrice = 14.95M,
              Director = "Johnny Smith"
            });
          tx.Commit();
        }

      }

      using (var session = sessionFactory.OpenStatelessSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var movies = GetMovies(session);
          foreach (var movie in movies)
          {
            UpdateMoviePrice(movie);
            session.Update(movie);
          }
          tx.Commit();
        }
      }
    }


    static IEnumerable<Movie> GetMovies(IStatelessSession session)
    {
      return session.CreateQuery("from Movie")
        .List<Movie>();
    }

    static Random rnd = new Random();

    static void UpdateMoviePrice(Movie movie)
    {
      // Random price between $9.95 and $24.95
      movie.UnitPrice = (decimal) rnd.Next(10, 26) - 0.05M;
    }

  }
}
