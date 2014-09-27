using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Hql;
using NHibernate.Criterion;
using NHibernate.Impl;
using Eg.Core;

namespace DetachedQueries
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var isbn = "3043";

      //var query = new DetachedNamedQuery("GetBookByISBN")
      //  .SetString("isbn", isbn);

      //var query = new DetachedQuery(hql)
      //  .SetString("isbn", isbn);

      var query = DetachedCriteria.For<Book>()
        .Add(Restrictions.Eq("ISBN", isbn));

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var book = query.GetExecutableCriteria(session)
            .UniqueResult<Book>();
          tx.Commit();
        }
      }
    }
  }
}
