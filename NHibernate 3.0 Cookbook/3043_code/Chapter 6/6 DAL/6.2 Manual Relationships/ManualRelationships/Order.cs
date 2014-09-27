using System.Collections.Generic;
using System;

namespace ManualRelationships
{
  public class Order
  {

    public virtual Guid Id { get; protected set; }

    public Order()
    {
      _items = new HashSet<OrderItem>();
    }

    private ICollection<OrderItem> _items;
    public virtual IEnumerable<OrderItem> Items
    {
      get
      {
        return _items;
      }
    }

    public virtual void AddItem(OrderItem newItem)
    {
      if (newItem != null && !_items.Contains(newItem))
      {
        _items.Add(newItem);
        newItem.SetOrder(this);
      }
    }

    public virtual void RemoveItem(OrderItem itemToRemove)
    {
      if (itemToRemove != null && _items.Remove(itemToRemove))
        itemToRemove.SetOrder(null);
    }

  }
}
