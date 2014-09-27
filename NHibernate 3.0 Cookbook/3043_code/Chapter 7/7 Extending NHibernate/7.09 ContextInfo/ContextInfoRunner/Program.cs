using AuditTriggerExample.Audit;
using Eg.Core;
using NHibernate.Cfg;
using System;
using Ninject;
using Microsoft.Practices.ServiceLocation;
using ContextInfoExample;
using CommonServiceLocator.NinjectAdapter;

namespace ContextInfoRunner
{
  class Program
  {

    static void Main(string[] args)
    {

      var kernel = new StandardKernel();
      kernel.Bind<IContextDataProvider>()
        .To<UsernameContextDataProvider>();
      var sl = new NinjectServiceLocator(kernel);
      ServiceLocator.SetLocatorProvider(() => sl);

      var namingStrategy = new NamingStrategy();
      var auditColumnSource = new CtxAuditColumnSource();
      var cfg = new Configuration().Configure();
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
          padlockId = (Guid)session.Save(padlock);
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
