using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping;

namespace AuditTriggerExample.Audit
{

  public class AuditColumn : Column
  {

    public bool IncludeInPrimaryKey { get; set; }

    public Func<TriggerActions, string> ValueFunction { get; set; }

  }

}
