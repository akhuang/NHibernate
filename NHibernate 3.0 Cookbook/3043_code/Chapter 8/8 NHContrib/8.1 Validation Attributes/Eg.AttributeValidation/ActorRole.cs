using NHibernate.Validator.Constraints;

namespace Eg.AttributeValidation
{
  public class ActorRole : Entity
  {

    [NotNull, Length(Min = 1, Max=255)]
    public virtual string Actor { get; set; }

    [NotNull, Length(Min = 1, Max = 255)]
    public virtual string Role { get; set; }

  }
}
