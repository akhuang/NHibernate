using System.Collections.Generic;
using System.Linq;
using NHibernate.Dialect;
using NHibernate.Engine;
using NHibernate.Mapping;

namespace AuditTriggerExample.Audit
{

  public class AuditTable : AbstractAuxiliaryDatabaseObject 
  {

    protected readonly Table _auditTable;

    public AuditTable(Table dataTable, 
      INamingStrategy namingStrategy,
      IAuditColumnSource auditColumnSource)
    {
      _auditTable = BuildAuditTable(dataTable, 
        namingStrategy, auditColumnSource);
    }

    public override string SqlCreateString(Dialect dialect, 
      IMapping p, string defaultCatalog, string defaultSchema)
    {
      return _auditTable.SqlCreateString(
        dialect, p, defaultCatalog, defaultSchema);
    }

    public override string SqlDropString(Dialect dialect, 
      string defaultCatalog, string defaultSchema)
    {
      return _auditTable.SqlDropString(dialect,
        defaultCatalog, defaultSchema);
    }

    protected virtual Table BuildAuditTable(Table dataTable,
      INamingStrategy namingStrategy, 
      IAuditColumnSource auditColumnSource)
    {
      var auditTableName = namingStrategy.GetAuditTableName(dataTable);
      var auditColumns = auditColumnSource.GetAuditColumns(dataTable);
      var auditTable = new Table(auditTableName);
      CopyColumns(dataTable, auditTable);
      CopyPrimaryKey(dataTable, auditTable);
      AddAuditColumns(auditTable, auditColumns);
      return auditTable;
    }

    protected virtual void CopyColumns(Table dataTable, 
      Table auditTable)
    {
      foreach (var column in dataTable.ColumnIterator)
        auditTable.AddColumn((Column) column.Clone());
    }

    protected virtual void CopyPrimaryKey(Table dataTable, 
      Table auditTable)
    {
      if (dataTable.PrimaryKey != null)
      {
        var pk = new PrimaryKey();

        pk.AddColumns(
          from column in dataTable.PrimaryKey.ColumnIterator
          select auditTable.GetColumn(column));

        auditTable.PrimaryKey = pk;
      }
    }

    protected virtual void AddAuditColumns(Table auditTable, 
      IEnumerable<AuditColumn> auditColumns)
    {
      foreach (var column in auditColumns)
      {
        auditTable.AddColumn(column);
        if (column.IncludeInPrimaryKey)
          auditTable.PrimaryKey.AddColumn(column);
      }
    }

    internal Table GetTable()
    {
      return _auditTable;
    }

  }

}
