using System;
using log4net;
using log4net.Config;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Exceptions;
using NHibernate.Validator.Engine;
using Environment = NHibernate.Validator.Cfg.Environment;

namespace Eg.AttributeValidation.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      XmlConfigurator.Configure();
      var log = LogManager.GetLogger(typeof(Program));

      SetupNHibernateValidator();

      var cfg = new Configuration().Configure();
      cfg.Initialize();

      var sessionFactory = cfg.BuildSessionFactory();

      var schemaExport = new SchemaExport(cfg);
      schemaExport.Execute(true, true, false);
      
      var junk = new Product
                   {
                     Name = "Spiffy Junk",
                     Description = "Stuff we can't sell.",
                     UnitPrice = -1M
                   };

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          try
          {
            session.Save(junk);
            tx.Commit();
          }
          catch (InvalidStateException validationException)
          {
            var errors = validationException.GetInvalidValues();
            foreach (var error in errors)
              log.ErrorFormat("Error with property {0}: {1}",
                error.PropertyName, error.Message);
            tx.Rollback();
          }
        }
      }

    }

    private static ValidatorEngine GetValidatorEngine()
    {
      var validatorEngine = new ValidatorEngine();
      validatorEngine.Configure();
      return validatorEngine;
    }

    private static void SetupNHibernateValidator()
    {
      var validatorEngine = GetValidatorEngine();
      new BasicSharedEngineProvider(validatorEngine).UseMe();
    }

  }
}
