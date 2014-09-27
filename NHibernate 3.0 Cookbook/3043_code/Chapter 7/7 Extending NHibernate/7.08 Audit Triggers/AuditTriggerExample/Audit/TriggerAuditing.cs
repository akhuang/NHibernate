using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Dialect;

namespace AuditTriggerExample.Audit
{

  public class TriggerAuditing
  {

    private readonly Configuration _configuration;
    private readonly INamingStrategy _namingStrategy;
    private readonly IAuditColumnSource _columnSource;

    public TriggerAuditing(Configuration configuration,
      INamingStrategy namingStrategy,
      IAuditColumnSource columnSource)
    {
      _configuration = configuration;
      _namingStrategy = namingStrategy;
      _columnSource = columnSource;
    }

    public void Configure()
    {
      _configuration.BuildMappings();
      var properties = _configuration.Properties;
      var dialect = Dialect.GetDialect(properties);
      if (!(dialect is IExtendedDialect))
        throw new ApplicationException(
          "Dialect must implement IExtendedDialect to "
          + "create audit triggers");
      var mappings = _configuration.CreateMappings(dialect);
      AddAuditing(mappings);
    }

    private void AddAuditing(Mappings mappings)
    {
      var auditObjects = new List<IAuxiliaryDatabaseObject>();
      foreach (var table in mappings.IterateTables.ToArray()) 
      {
        var auditTable = new AuditTable(
          table, _namingStrategy, _columnSource);
        mappings.AddAuxiliaryDatabaseObject(auditTable);

        var insertTrigger = new AuditTrigger(table,
          auditTable, _namingStrategy, TriggerActions.INSERT);
        mappings.AddAuxiliaryDatabaseObject(insertTrigger);

        var updateTrigger = new AuditTrigger(table,
          auditTable, _namingStrategy, TriggerActions.UPDATE);
        mappings.AddAuxiliaryDatabaseObject(updateTrigger);

        var deleteTrigger = new AuditTrigger(table,
          auditTable, _namingStrategy, TriggerActions.DELETE);
        mappings.AddAuxiliaryDatabaseObject(deleteTrigger);
      }
      
    }

  }

}
