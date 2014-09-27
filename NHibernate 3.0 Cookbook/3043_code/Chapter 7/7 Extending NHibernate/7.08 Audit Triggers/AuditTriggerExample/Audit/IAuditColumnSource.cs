using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping;

namespace AuditTriggerExample.Audit
{

  public interface IAuditColumnSource
  {

    IEnumerable<AuditColumn> GetAuditColumns(Table dataTable);

  }

}
