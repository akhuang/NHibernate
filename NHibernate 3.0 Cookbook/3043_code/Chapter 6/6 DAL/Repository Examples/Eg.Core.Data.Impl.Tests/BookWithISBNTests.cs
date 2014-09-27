using Eg.Core.Data.Impl.Queries;
using Eg.Core.Data.Queries;
using NUnit.Framework;
using SQLiteTesting;


namespace Eg.Core.Data.Impl.Tests
{

  [TestFixture]
  public class BookWithISBNTests : DALFixture  
  {
    
    [Test]
    public void BookWithISBN_Test()
    {

      Book expected = new Book()
      {
        Name = "NHibernate 3.0 Cookbook",
        Description = "A collection of NHibernate recipes",
        UnitPrice = 45M,
        Author = "Jason Dentler",
        ISBN = "1234"
      };

      using (var tx = Session.BeginTransaction())
      {
        Session.SaveOrUpdate(expected);
        tx.Commit();
      }

      var repository = Locator.GetInstance<IRepository<Book>>();
      
      var query = repository.CreateQuery<IBookWithISBN>();
      query.ISBN = expected.ISBN;
      Book actual = query.Execute();

      Assert.AreSame(expected, actual);

    }

  }
}
