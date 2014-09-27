using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Linq;
using Eg.Core;

namespace NHProfTest
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();

      HibernatingRhinos.Profiler.Appender.
        NHibernate.NHibernateProfiler.Initialize();

      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      using (var session = sessionFactory.OpenSession())
      {
        var books = from b in session.Query<Book>()
                   where b.Author == "Jason Dentler"
                   select b;

        //using (var tx = session.BeginTransaction())
        //{
          foreach (var book in books)
            Console.WriteLine(book.Name);
          //tx.Commit();
        //}
      }
    }




  }
}
