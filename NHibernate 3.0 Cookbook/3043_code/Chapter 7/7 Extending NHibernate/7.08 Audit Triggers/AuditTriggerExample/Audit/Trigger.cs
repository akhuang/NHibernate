using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping;
using NHibernate.Dialect;
using NHibernate.Engine;

namespace AuditTriggerExample.Audit
{

  public abstract class Trigger : 
    AbstractAuxiliaryDatabaseObject 
  {

    protected readonly string _triggerName;
    protected readonly TriggerActions _action;
    protected readonly string _tableName;

    public Trigger(string triggerName, string tableName, TriggerActions action)
    {
      _action = action;
      _triggerName = triggerName;
      _tableName = tableName;
    }

    public abstract string SqlTriggerBody(Dialect dialect, IMapping p, string defaultCatalog, string defaultSchema);

    public override string SqlCreateString(Dialect dialect, IMapping p, string defaultCatalog, string defaultSchema)
    {
      IExtendedDialect eDialect = (IExtendedDialect) dialect;

      var buf = new StringBuilder();
      
      buf.AppendLine(eDialect.GetCreateTriggerHeaderString(
        _triggerName, _tableName, _action));

      buf.AppendLine(SqlTriggerBody(dialect, p, defaultCatalog, defaultSchema));

      buf.AppendLine(eDialect.GetCreateTriggerFooterString(
        _triggerName, _tableName, _action));

      return buf.ToString();
    }

    public override string SqlDropString(Dialect dialect, string defaultCatalog, string defaultSchema)
    {
      var eDialect = (IExtendedDialect)dialect;
      return eDialect.GetDropTriggerString(
        _triggerName, _tableName, _action);
    }

  }

}
