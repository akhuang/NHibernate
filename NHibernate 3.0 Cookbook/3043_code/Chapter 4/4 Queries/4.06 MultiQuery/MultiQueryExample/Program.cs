using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using Eg.Core;

namespace MultiQueryExample
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
          int pageNumber = 3;
          int pageSize = 10;

          var movieCountQuery = session.CreateQuery(
            @"select count(m.Id) from Movie m");

          var moviePageQuery = session.CreateQuery(
            @"from Movie m")
            .SetFirstResult((pageNumber - 1) * pageSize)
            .SetMaxResults(pageSize);

          var multiQuery = session.CreateMultiQuery()
            .Add<Int64>("count", movieCountQuery)
            .Add<Movie>("page", moviePageQuery);

          var countResult = ((IList<Int64>)multiQuery
            .GetResult("count")).Single();
          
          var pageResult = (IList<Movie>)multiQuery
            .GetResult("page");

          tx.Commit();

        }
      }

    }
  }
}
