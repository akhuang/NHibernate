using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.SqlTypes;

namespace WKITExample
{

  public class RegionType 
    : WellKnownInstanceType<Region>
  {

    private static readonly SqlType[] sqlTypes = 
      new[] { SqlTypeFactory.Int32 };

    public RegionType()
      : base(new Regions(), 
      (entity, id) => entity.Id == id,
      entity => entity.Id)
    { }

    public override NHibernate.SqlTypes.SqlType[] SqlTypes
    {
      get { return sqlTypes; }
    }

  }

}
