using AuditTriggerExample.Audit;
using Eg.Core;
using NHibernate.Cfg;
using System;

namespace AuditTriggerRunner
{
  class Program
  {
    static void Main(string[] args)
    {
      var cfg = new Configuration().Configure();

      var namingStrategy = new NamingStrategy();
      var auditColumnSource = new AuditColumnSource();
      new TriggerAuditing(cfg, namingStrategy,
        auditColumnSource).Configure();
      
      var sessionFaculty = cfg.BuildSessionFactory();

      var se = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);
      se.Execute(true, true, false);
      
      var padlock = new Product()
          {
            Name = "Padlock",
            Description = "Secure, weather resistant",
            UnitPrice = 8.36M
          };
      Guid padlockId;

      using (var session = sessionFaculty.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          padlockId = (Guid) session.Save(padlock);
          tx.Commit();
        }
      }

      using (var session = sessionFaculty.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          padlock = session.Get<Product>(padlockId);
          padlock.UnitPrice = 0.10M;
          padlock.Description = "Not so secure, actually.";
          session.SaveOrUpdate(padlock);
          tx.Commit();
        }
      }

      using (var session = sessionFaculty.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          padlock = session.Load<Product>(padlockId);
          session.Delete(padlock);
          tx.Commit();
        }
      }



    }
  }
}
