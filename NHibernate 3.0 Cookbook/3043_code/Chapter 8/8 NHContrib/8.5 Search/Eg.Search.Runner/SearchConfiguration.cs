using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Search.Event;
using NHibernate.Search.Store;

namespace Eg.Search.Runner
{
  public class SearchConfiguration
  {

    public ISessionFactory BuildSessionFactory()
    {
      var cfg = new Configuration().Configure();
      SetSearchProps(cfg);
      AddSearchListeners(cfg);
      var sessionFactory = cfg.BuildSessionFactory();
      return new SessionFactorySearchWrapper(
        sessionFactory);
    }

    private void SetSearchProps(Configuration cfg)
    {
      cfg.SetProperty(
        "hibernate.search.default.directory_provider", 
        typeof(FSDirectoryProvider)
        .AssemblyQualifiedName);

      cfg.SetProperty(
        "hibernate.search.default.indexBase",
        "~/Index");
    }

    private void AddSearchListeners(Configuration cfg)
    {
      cfg.SetListener(ListenerType.PostUpdate, 
        new FullTextIndexEventListener());
      cfg.SetListener(ListenerType.PostInsert, 
        new FullTextIndexEventListener());
      cfg.SetListener(ListenerType.PostDelete, 
        new FullTextIndexEventListener());
      cfg.SetListener(ListenerType.PostCollectionRecreate, 
        new FullTextIndexCollectionEventListener());
      cfg.SetListener(ListenerType.PostCollectionRemove, 
        new FullTextIndexCollectionEventListener());
      cfg.SetListener(ListenerType.PostCollectionUpdate, 
        new FullTextIndexCollectionEventListener());
    }
  }
}
