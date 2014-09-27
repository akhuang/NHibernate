using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using Eg.Core;
using System.Diagnostics;

namespace PagingResults
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
          int pageNumber = 1;
          int pageSize = 10;

          var countValue = session
            .GetNamedQuery("CountAllMovies")
            .FutureValue<Int64>();

          var pageResults = session
            .GetNamedQuery("GetAllMovies")
            .SetFirstResult((pageNumber - 1) * pageSize)
            .SetMaxResults(pageSize)
            .Future<Movie>();

          Console.WriteLine("{0} movies total.", 
            countValue.Value);

          foreach (var movie in pageResults)
            Console.WriteLine(movie.Name);

          Console.ReadKey();
          tx.Commit();
        }
      }
    }
  }
}
