using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Eg.Core.Data.Impl.Tests
{

  [TestFixture]
  public class RepositoryTests : DALFixture 
  {

    [Test]
    public void GetEnumerator_enumerates_less_than_1000_entities()
    {
      Book book = new Book()
        {
          Name = "NHibernate 3.0 Cookbook",
          Description = "Awesome book",
          UnitPrice = 49.95M,
          ISBN = "1235",
          Author = "Jason Dentler"
        };

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(book);
        tx.Commit();
      }

      var repo = Locator.GetInstance<IRepository<Book>>();

      foreach (var b in repo)
        Assert.AreSame(book, b);

    }

    [Test]
    public void GetEnumerator_enumerates_no_more_than_1000_entities()
    {
      using (var tx = Session.BeginTransaction())
      {
        for (int i = 1; i < 1010; i++)
          Session.Save(new Book()
          {
            Name = string.Format("Book #{0}", i),
            Description = string.Format("Book #{0}", i),
            UnitPrice = 3M,
            ISBN = i.ToString(),
            Author = "Joe Writer"
          });
        tx.Commit();
      }

      var repo = Locator.GetInstance<IRepository<Book>>();
      int count = 0;
      foreach (var b in repo)
        count++;
      Assert.AreEqual(1000, count);
    }


  }

}
