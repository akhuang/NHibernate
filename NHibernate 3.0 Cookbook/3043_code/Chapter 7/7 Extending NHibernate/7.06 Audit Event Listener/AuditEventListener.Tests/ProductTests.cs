using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using SQLiteTesting;
using Eg.Core;

namespace AuditEventListener.Tests
{

  [TestFixture]
  public class ProductTests : NHibernateFixture 
  {

    [Test]
    public void PersistenceTest()
    {
      Guid productId;
      using (var session = SessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          productId = (Guid)session.Save(new Product()
          {
            Name = "S'mores Kit",
            Description = "Campfire not included",
            UnitPrice = 9.97M
          });
          tx.Commit();
        }
      }

      using (var session = SessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var product = session.Get<Product>(productId);
          // 10% off
          product.UnitPrice *= 0.90M;
          tx.Commit();
        }
      }

      using (var session = SessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var product = session.Load<Product>(productId);
          session.Delete(product);
          tx.Commit();
        }
      }

    }

  }

}
