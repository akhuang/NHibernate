using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQLiteTesting;

namespace Changestamp.Tests
{
  
  [TestFixture]
  public class ProductTests : NHibernateFixture 
  {

    [Test]
    public void CreateStampTest()
    {

      Guid productId;

      var product = new Product()
      {
        Name = "Firewood",
        Description = "3 logs. Great for S'mores!",
        UnitPrice = 10M
      };

      using (var tx = Session.BeginTransaction())
      {
        productId = (Guid)Session.Save(product);
        tx.Commit();
      }

      Assert.IsNotNullOrEmpty(product.CreatedBy);
      Assert.AreNotEqual(default(DateTime), product.CreatedTS);

    }

    [Test]
    public void ChangeStampTest()
    {

      Guid productId;

      var product = new Product()
      {
        Name = "Firewood",
        Description = "3 logs. Great for S'mores!",
        UnitPrice = 10M
      };

      using (var tx = Session.BeginTransaction())
      {
        productId = (Guid)Session.Save(product);
        tx.Commit();
      }

      product.ChangedBy = string.Empty;
      System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

      using (var tx = Session.BeginTransaction())
      {
        product.UnitPrice *= 0.9M;
        Session.SaveOrUpdate(product);
        tx.Commit();
      }

      Assert.IsNotNullOrEmpty(product.ChangedBy);
      Assert.AreNotEqual(product.CreatedTS, product.ChangedTS);

    }

  }

}
