using System;
using System.Collections.Generic;
using System.Diagnostics;
using NHibernate;
using NHibernate.Cfg;

namespace EntityModeMap
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var movieActors = new List<Dictionary<string, object>>()
      {
        new Dictionary<string, object>() {
          {"Actor","Keanu Reeves"},
          {"Role","Neo"}
        },
        new Dictionary<string, object>() {
          {"Actor", "Carrie-Ann Moss"},
          {"Role", "Trinity"}
        }
      };

      var movie = new Dictionary<string, object>()
      {
        {"Name", "The Matrix"},
        {"Description", "Sci-Fi Action film"},
        {"UnitPrice", 18.99M},
        {"Director", "Wachowski Brothers"},
        {"Actors", movieActors}
      };

      Guid movieId;
      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          movieId = (Guid) session.Save("Movie", movie);
          tx.Commit();
        }
      }
    }
  }
}
