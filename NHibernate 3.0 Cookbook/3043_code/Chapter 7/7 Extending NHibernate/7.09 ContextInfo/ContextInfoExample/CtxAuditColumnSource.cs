﻿using System;
using System.Collections.Generic;
using AuditTriggerExample.Audit;
using NHibernate;
using NHibernate.Mapping;

namespace ContextInfoExample
{

  public class CtxAuditColumnSource : IAuditColumnSource 
  {


    public IEnumerable<AuditColumn> GetAuditColumns(Table dataTable)
    {
      var userStamp = new AuditColumn()
      {
        Name = "AuditUser",
        Value = new SimpleValue()
        {
          TypeName = NHibernateUtil.String.Name
        },
        Length = 127,
        IsNullable = false,
        IncludeInPrimaryKey = true,
        ValueFunction = delegate(TriggerActions action)
        {
          return "dbo.fnGetContextData()";
        }
      };

      var timeStamp = new AuditColumn()
      {
        Name = "AuditTimestamp",
        Value = new SimpleValue()
        {
          TypeName = NHibernateUtil.DateTime.Name
        },
        IsNullable = false,
        IncludeInPrimaryKey = true,
        ValueFunction = delegate(TriggerActions action)
        {
          return "getdate()";
        }
      };

      var operation = new AuditColumn()
      {
        Name = "AuditOperation",
        Value = new SimpleValue()
        {
          TypeName = NHibernateUtil.AnsiChar.Name
        },
        Length = 1,
        IsNullable = false,
        IncludeInPrimaryKey = false,
        ValueFunction = delegate(TriggerActions action)
        {
          switch (action)
          {
            case TriggerActions.INSERT:
              return "'I'";
            case TriggerActions.UPDATE:
              return "'U'";
            case TriggerActions.DELETE:
              return "'D'";
            default:
              throw new ArgumentOutOfRangeException("action");
          }
        }
      };

      return new AuditColumn[] {
        userStamp, timeStamp, operation 
      };

    }

  }

}
