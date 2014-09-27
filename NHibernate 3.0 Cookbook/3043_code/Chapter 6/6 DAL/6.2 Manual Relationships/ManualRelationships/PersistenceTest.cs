using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteTesting;
using NUnit.Framework;

namespace ManualRelationships
{

  [TestFixture]
  public class PersistenceTest : NHibernateFixture 
  {

    [Test]
    public void Order_cascades_to_OrderItem()
    {

      var order = new Order();
      var item = new OrderItem();
      order.AddItem(item);

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(order);
        tx.Commit();
      }

      Session.Clear();

      using (var tx = Session.BeginTransaction())
      {
        order = Session.Get<Order>(order.Id);
        tx.Commit();
      }

      Assert.True(order.Items.Count() == 1);

    }

  }

}
