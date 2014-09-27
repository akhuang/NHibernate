using System;
using System.Collections.Generic;
using System.Linq;
using Eg.Core;
using NHibernate.Cfg;

namespace MultiCriteriaExample
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      int pageNumber = 3;
      int pageSize = 10;

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {

          var movieCountQuery = session.QueryOver<Movie>()
            .Select(list => list
              .SelectCount(m => m.Id));
          
          var moviePageQuery = session.QueryOver<Movie>()
            .OrderBy(m => m.UnitPrice).Asc
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

          var multiCrit = session.CreateMultiCriteria()
            .Add<int>("count", movieCountQuery.UnderlyingCriteria)
            .Add<Movie>("page", moviePageQuery.UnderlyingCriteria);

          var countResult = ((IList<int>)multiCrit
            .GetResult("count")).Single();

          var pageResult = (IList<Movie>) multiCrit
            .GetResult("page");

          tx.Commit();

        }
      }

    }
  }
}
