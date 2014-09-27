using System;

namespace Changestamp
{
  public class Product : Entity
  {

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual Decimal UnitPrice { get; set; }

  }
}
