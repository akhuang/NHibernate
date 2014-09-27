using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ManualRelationships
{
  [TestFixture]
  public class OrderRelationshipTests
  {

    [Test]
    public void SetOrder_instance_adds_to_Order_Items()
    {
      var order = new Order();
      var item = new OrderItem();
      item.SetOrder(order);
      Assert.AreSame(order, item.Order);
      Assert.That(order.Items.Contains(item));
      Assert.AreEqual(1, order.Items.Count());
    }

    [Test]
    public void SetOrder_null_removes_from_order_Items()
    {
      var order = new Order();
      var item = new OrderItem();
      item.SetOrder(order);
      item.SetOrder(null);
      
      Assert.IsNull(item.Order);
      Assert.False(order.Items.Any());
    }

    [Test]
    public void AddItem_sets_Item_Order()
    {
      var order = new Order();
      var item = new OrderItem();
      
      order.AddItem(item);
      
      Assert.AreSame(order, item.Order);
      Assert.That(order.Items.Contains(item));
      Assert.AreEqual(1, order.Items.Count());
    }

    [Test]
    public void RemoveItem_sets_Item_Order_null()
    {
      var order = new Order();
      var item = new OrderItem();
      order.AddItem(item);
      order.RemoveItem(item);
      Assert.IsNull(item.Order);
      Assert.False(order.Items.Any());
    }

    [Test]
    public void AddRemoteItem_can_switch_orders()
    {
      var order1 = new Order();
      var order2 = new Order();
      var item = new OrderItem();
      order1.AddItem(item);
      Assert.AreSame(order1, item.Order);
      Assert.True(order1.Items.Contains(item));
      Assert.False(order2.Items.Any());

      order1.RemoveItem(item);
      order2.AddItem(item);

      Assert.AreSame(order2, item.Order);
      Assert.True(order2.Items.Contains(item));
      Assert.AreEqual(1, order2.Items.Count());
      Assert.False(order1.Items.Any());
    }

    [Test]
    public void SetOrder_can_switch_orders()
    {
      var order1 = new Order();
      var order2 = new Order();
      var item = new OrderItem();
      order1.AddItem(item);
      Assert.AreSame(order1, item.Order);
      Assert.True(order1.Items.Contains(item));
      Assert.False(order2.Items.Any());

      item.SetOrder(order2);

      Assert.AreSame(order2, item.Order);
      Assert.True(order2.Items.Contains(item));
      Assert.AreEqual(1, order2.Items.Count());
      Assert.False(order1.Items.Any());
    }

  }


}
