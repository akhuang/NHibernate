using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping;
using NHibernate.Dialect;
using NHibernate.Engine;

namespace AuditTriggerExample.Audit
{

  public class AuditTrigger : 
    Trigger  
  {

    protected readonly string _auditTableName;
    protected readonly string[] _dataColumnNames;
    protected readonly AuditColumn[] _auditColumns;

    public AuditTrigger(Table dataTable, AuditTable auditTable,
      INamingStrategy namingStrategy, TriggerActions action)
      : base(
      namingStrategy.GetTriggerName(dataTable, action), 
      dataTable.GetQuotedName(),
      action)
    {
      _auditTableName = auditTable.GetTable().Name;

      _dataColumnNames = (
        from column in dataTable.ColumnIterator
        select column.GetQuotedName()
        ).ToArray();

      _auditColumns = (
        from column in auditTable.GetTable().ColumnIterator
        where column is AuditColumn
        select column as AuditColumn 
        ).ToArray();

    }

    public override string SqlTriggerBody(
      Dialect dialect, IMapping p, 
      string defaultCatalog, string defaultSchema)
    {
      var auditTableName = dialect.QuoteForTableName(_auditTableName);
      var eDialect = (IExtendedDialect)dialect;

      string triggerSource = _action == TriggerActions.DELETE ?
        eDialect.GetTriggerOldDataAlias() :
        eDialect.GetTriggerNewDataAlias();

      var columns = new List<string>(_dataColumnNames);
      columns.AddRange(from ac in _auditColumns
                       select ac.Name);

      var values = new List<string>();
      values.AddRange(
        from columnName in _dataColumnNames
        select eDialect.QualifyColumn(
          triggerSource, columnName));
      values.AddRange(
        from auditColumn in _auditColumns 
        select auditColumn.ValueFunction.Invoke(_action));

      return eDialect.GetInsertIntoString(auditTableName, 
        columns, triggerSource, values);

    }



  }

}
