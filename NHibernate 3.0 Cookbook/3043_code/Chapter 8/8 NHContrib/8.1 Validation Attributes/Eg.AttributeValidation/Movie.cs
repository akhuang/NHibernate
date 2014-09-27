using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Eg.AttributeValidation
{
  public class Movie : Product
  {

    [NotNull, Length(Min=1, Max=255)]
    public virtual string Director { get; set; }

    [Valid]
    public virtual IList<ActorRole> Actors { get; set; }

  }
}
