using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActionFilterExample
{
  public static class DataAccessLayer
  {

    public static IEnumerable<Eg.Core.Book> GetBooks()
    {
      var session = MvcApplication.SessionFactory.GetCurrentSession();
      using (var tx = session.BeginTransaction())
      {
        var books = session.QueryOver<Eg.Core.Book>().List();
        tx.Commit();
        return books;
      }
    }

  }
}
