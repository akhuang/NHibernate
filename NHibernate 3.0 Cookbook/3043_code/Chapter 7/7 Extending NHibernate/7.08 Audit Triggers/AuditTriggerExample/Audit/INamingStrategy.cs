using NHibernate.Mapping;

namespace AuditTriggerExample.Audit
{

  public interface INamingStrategy
  {

    string GetAuditTableName(Table dataTable);
    string GetTriggerName(Table dataTable, TriggerActions action);

  }

}
