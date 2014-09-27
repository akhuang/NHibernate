using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.SqlTypes;

namespace WKITExample
{

  public class StateType 
    : GenericWellKnownInstanceType<State, string>
  {

    private static readonly SqlType[] sqlTypes = 
      new[] { SqlTypeFactory.GetString(2)};

    public StateType()
      : base(new States(), 
      (entity, id) => entity.PostalCode == id,
      entity => entity.PostalCode)
    { }

    public override NHibernate.SqlTypes.SqlType[] SqlTypes
    {
      get { return sqlTypes; }
    }

  }

}
