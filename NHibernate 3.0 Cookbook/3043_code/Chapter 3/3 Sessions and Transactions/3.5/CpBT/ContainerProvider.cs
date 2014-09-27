using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CpBT.DataAccess;
using CpBT.Models;
using NHibernate;
using NHibernate.Engine;
using uNhAddIns.CastleAdapters;
using uNhAddIns.CastleAdapters.AutomaticConversationManagement;
using uNhAddIns.SessionEasier;
using uNhAddIns.SessionEasier.Conversations;

namespace CpBT
{
  public static class ContainerProvider
  {

    private static readonly IWindsorContainer _container;

    public static IWindsorContainer Container
    {
      get
      {
        return _container;
      }
    }

    static ContainerProvider()
    {
      _container = new WindsorContainer();
      _container.AddFacility<PersistenceConversationFacility>();
      _container.AddFacility<FactorySupportFacility>();

      _container.Register(
        Component.For<ISessionFactoryProvider>()
        .ImplementedBy<SessionFactoryProvider>());

      _container.Register(
        Component.For<ISessionFactory>().UsingFactoryMethod(
          () => _container
            .Resolve<ISessionFactoryProvider>().GetFactory(null))
        );

      _container.Register(
        Component.For<ISessionFactoryImplementor>()
        .UsingFactoryMethod(
          () => (ISessionFactoryImplementor)_container
            .Resolve<ISessionFactoryProvider>().GetFactory(null))
        );

      _container.Register(
        Component.For<ISessionWrapper>()
        .ImplementedBy<SessionWrapper>());

      _container.Register(
        Component.For<IConversationFactory>()
        .ImplementedBy<DefaultConversationFactory>());

      _container.Register(
        Component.For<IConversationsContainerAccessor>()
        .ImplementedBy<NhConversationsContainerAccessor>());

      _container.Register(
        Component.For(typeof(IDao<>))
          .ImplementedBy(typeof(DaoImpl<>)));

      _container.Register(
        Component.For<IEditMovieModel>()
          .ImplementedBy<EditMovieModel>()
          .LifeStyle.Transient);
    }

  }
}
