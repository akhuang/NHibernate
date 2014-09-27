using NHibernate.Validator.Constraints;

namespace Eg.AttributeValidation
{
  public class Book : Product
  {

    [NotNull, Pattern(@"^[\d]{9}[\dxX]$")]
    public virtual string ISBN { get; set; }

    [NotNull, Length(Min=1, Max=255)]
    public virtual string Author { get; set; }

  }
}
