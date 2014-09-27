using System.Collections.Generic;
using NHibernate.Search.Attributes;

namespace Eg.Search
{

  [Indexed]
  public class Movie : Product
  {

    [Field]
    public virtual string Director { get; set; }

    public virtual IList<ActorRole> Actors { get; set; }

  }
}
