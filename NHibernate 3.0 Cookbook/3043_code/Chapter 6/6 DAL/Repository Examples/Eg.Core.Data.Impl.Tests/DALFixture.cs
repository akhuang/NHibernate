using System.Linq;
using CommonServiceLocator.NinjectAdapter;
using Eg.Core.Data.Impl.Queries;
using Eg.Core.Data.Queries;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Ninject;
using SQLiteTesting;

namespace Eg.Core.Data.Impl.Tests
{

  public abstract class DALFixture : NHibernateFixture
  {
    private IServiceLocator _locator; 
    public IServiceLocator Locator
    {
      get
      {
        return _locator;
      }
    }

    protected override void OnFixtureSetup()
    {
      base.OnFixtureSetup();
      SetupServiceLocator();
    }

    private void SetupServiceLocator()
    {
      var kernel = new StandardKernel();

      kernel.Bind<ISessionFactory>().ToConstant(SessionFactory);
      kernel.Bind<IQueryFactory>().To<QueryFactoryImpl>();
      kernel.Bind<IServiceLocator>()
        .ToMethod(ctx => Locator);
      kernel.Bind(typeof(IRepository<>))
        .To(typeof(NHibernateRepository<>));
      BindQueries(kernel);

      _locator = new NinjectServiceLocator(kernel);
    }

    private void BindQueries(IKernel kernel)
    {
      var asm = typeof(BookWithISBN).Assembly;
      var queryType = typeof(Eg.Core.Data.Queries.IQuery);
      var queryMap = from t in asm.GetTypes()
                     where queryType.IsAssignableFrom(t)
                     && t.IsClass
                     && !t.IsAbstract
                     let entry = new
                     {
                       implementation = t,
                       service = (from i in t.GetInterfaces()
                                where i.Name == "I" + t.Name
                                select i)
                                .SingleOrDefault()
                     }
                     where entry.service != null
                     select entry;

      foreach (var entry in queryMap)
        kernel.Bind(entry.service).To(entry.implementation);

    }

  }

}
