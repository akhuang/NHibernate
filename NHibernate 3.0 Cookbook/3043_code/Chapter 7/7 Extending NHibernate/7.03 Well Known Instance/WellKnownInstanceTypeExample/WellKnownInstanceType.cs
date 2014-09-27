using System;
using System.Collections.Generic;
using NHibernate.SqlTypes;

namespace WKITExample
{
  [Serializable]
  public abstract class WellKnownInstanceType<T> : GenericWellKnownInstanceType<T, long> where T : class
  {
    private static readonly SqlType[] sqlTypes = new[] { SqlTypeFactory.Int64 };

    protected WellKnownInstanceType(
      IEnumerable<T> repository, 
      Func<T, long, bool> findPredicate, 
      Func<T, long> idGetter)
      : base(repository, findPredicate, idGetter)
    {
    }

    public override SqlType[] SqlTypes
    {
      get { return sqlTypes; }
    }
  }
}
