using Eg.Core;
using NHibernate.Validator.Cfg.Loquacious;

namespace Eg.ClassValidation
{

  public class ActorValidation : ValidationDef<ActorRole>
  {
    
    public ActorValidation()
    {
      Define(ar => ar.Actor)
        .NotNullableAndNotEmpty()
        .And.MaxLength(255);

      Define(ar => ar.Role)
        .NotNullableAndNotEmpty()
        .And.MaxLength(255);
    }

  }
}
