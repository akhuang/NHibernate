using System.Reflection;
using Eg.Core;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;
using Environment = NHibernate.Validator.Cfg.Environment;

namespace Eg.ClassValidation.Runner
{

	class Program
	{
		private static void Main(string[] args)
		{
			XmlConfigurator.Configure();
			var log = LogManager.GetLogger(typeof (Program));

		  SetupNHibernateValidator();

			var nhibernateConfig = new Configuration().Configure();
			nhibernateConfig.Initialize(); 

			ISessionFactory sessionFactory = nhibernateConfig.BuildSessionFactory();

			var schemaExport = new SchemaExport(nhibernateConfig);
			schemaExport.Execute(false, true, false);

			var junk = new Product
			           	{
			           		Name = "Spiffy Junk",
			           		Description = string.Empty,
			           		UnitPrice = -1M
			           	};

		  var ve = Environment.SharedEngineProvider.GetEngine();
		  var invalidValues = ve.Validate(junk);
      foreach (var invalidValue in invalidValues)
        log.InfoFormat("{0} {1}",
          invalidValue.PropertyName,
          invalidValue.Message);

		}

    private static FluentConfiguration GetNhvConfiguration()
    {
      var nhvConfiguration = new FluentConfiguration();
      nhvConfiguration
        .SetDefaultValidatorMode(ValidatorMode.UseExternal)
        .Register(Assembly.Load("Eg.ClassValidation")
                    .ValidationDefinitions())
        .IntegrateWithNHibernate
        .ApplyingDDLConstraints()
        .And
        .RegisteringListeners();
      return nhvConfiguration;
    }

    private static ValidatorEngine GetValidatorEngine()
    {
      var cfg = GetNhvConfiguration();
      var validatorEngine = new ValidatorEngine();
      validatorEngine.Configure(cfg);
      return validatorEngine;
    }

    private static void SetupNHibernateValidator()
    {
      var validatorEngine = GetValidatorEngine();
      new BasicSharedEngineProvider(validatorEngine).UseMe();
    }

	}
}