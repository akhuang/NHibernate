using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;

namespace ExecutableHQL
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
          var hql = @"update Book b 
                     set b.UnitPrice = :minPrice
                     where b.UnitPrice < :minPrice";
          session.CreateQuery(hql)
            .SetDecimal("minPrice", 5M)
            .ExecuteUpdate();

          tx.Commit();
        }
      }
    }
  }
}
