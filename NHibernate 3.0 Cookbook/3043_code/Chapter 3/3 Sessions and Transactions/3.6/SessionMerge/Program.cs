using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using log4net;
using Eg.Core;

namespace SessionMerge
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var book = CreateAndSaveBook(sessionFactory);
      book.Name = "Dormice in Action";
      book.Description = "Hibernation of the Hazel Dormouse";
      book.UnitPrice = 0.83M;
      book.ISBN = "0123";

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var mergedBook = (Book) session.Merge(book);
          tx.Commit();
        }
      }


    }

    private static Book CreateAndSaveBook(
      ISessionFactory sessionFactory)
    {
      var book = new Book()
      {
        Name = "NHibernate 3.0 Cookbook",
        Description = "Pure Awesome.",
        UnitPrice = 50.0M,
        ISBN = "3043",
        Author = "Jason Dentler",
      };

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          session.Save(book);
          tx.Commit();
        }
      }

      return book;

    }


  }
}
