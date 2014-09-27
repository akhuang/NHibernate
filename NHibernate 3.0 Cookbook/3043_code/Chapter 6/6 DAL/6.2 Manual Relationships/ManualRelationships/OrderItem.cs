using System;

namespace ManualRelationships
{
  public class OrderItem
  {

    public virtual Guid Id { get; protected set; }

    public virtual Order Order { get; protected set; }

    public virtual void SetOrder(Order newOrder)
    {
      var prevOrder = Order;

      if (newOrder == prevOrder)
        return;

      Order = newOrder;

      if (prevOrder != null)
        prevOrder.RemoveItem(this);

      if (newOrder != null)
        newOrder.AddItem(this);

    }

  }
}
