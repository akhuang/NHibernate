using System;
using NHibernate.Search.Attributes;

namespace Eg.Search
{

  [Indexed]
  public class Product : Entity
  {

    [Field]
    public virtual string Name { get; set; }

    [Field]
    public virtual string Description { get; set; }
    public virtual Decimal UnitPrice { get; set; }

  }
}
