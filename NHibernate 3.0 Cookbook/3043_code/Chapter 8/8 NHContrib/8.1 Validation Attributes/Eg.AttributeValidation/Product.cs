using System;
using NHibernate.Validator.Constraints;

namespace Eg.AttributeValidation
{
  public class Product : Entity
  {

    [NotNull, Length(Min=1, Max=255)]
    public virtual string Name { get; set; }

    [NotNullNotEmpty]
    public virtual string Description { get; set; }

    [NotNull, NotNegativeDecimal]
    public virtual Decimal UnitPrice { get; set; }

  }
}
