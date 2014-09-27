using System;

namespace BaseEntityExample
{
  public class Product : Entity<Product>
  {

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual Decimal UnitPrice { get; set; }

    public override bool Equals(Product other)
    {
      if (other == null)
        return false;
      return Equals(Name, other.Name);
    }

    public override int GetHashCode()
    {
      return (Name ?? string.Empty).GetHashCode();
    }

  }
}
