using System.Collections.Generic;
using log4net;
using NHibernate;
using NHibernate.Proxy;
using NHibernate.Type;

namespace GhostbustersTest
{
  public class GhostInterceptor : EmptyInterceptor
  {

    private static readonly ILog log = 
      LogManager.GetLogger(typeof(GhostInterceptor));

    private readonly IList<string> _ghosts;
    private ISession _session;

    public GhostInterceptor(IList<string> ghosts)
    {
      _ghosts = ghosts;
    }

    public override void SetSession(ISession session)
    {
      _session = session;
    }

    public override bool OnFlushDirty(
    object entity, object id, object[] currentState,
    object[] previousState, string[] propertyNames, IType[] types)
    {
      var msg = string.Format("Flush Dirty {0}", 
        entity.GetType().FullName);
      log.Error(msg);
      _ghosts.Add(msg);
      ListDirtyProperties(entity);
      return false;
    }

    public override bool OnSave(
    object entity, object id, object[] state,
    string[] propertyNames, IType[] types)
    {
      var msg = string.Format("Save {0}", 
        entity.GetType().FullName);
      log.Error(msg);
      _ghosts.Add(msg);
      return false;
    }

    public override void OnDelete(
    object entity, object id, object[] state,
    string[] propertyNames, IType[] types)
    {
      var msg = string.Format("Delete {0}", 
        entity.GetType().FullName);
      log.Error(msg);
      _ghosts.Add(msg);
    }

    private void ListDirtyProperties(object entity)
    {
      string className = NHibernateProxyHelper.GuessClass(entity).FullName;
      var sessionImpl = _session.GetSessionImplementation();
      var persister = sessionImpl.Factory.GetEntityPersister(className);
      var oldEntry = sessionImpl.PersistenceContext.GetEntry(entity);

      if ((oldEntry == null) && (entity is INHibernateProxy))
      {
        var proxy = entity as INHibernateProxy;
        object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
        oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
      }

      object[] oldState = oldEntry.LoadedState;

      object[] currentState = persister.GetPropertyValues(entity, 
        sessionImpl.EntityMode);

      int[] dirtyProperties = persister.FindDirty(currentState, 
        oldState, entity, sessionImpl);

      foreach (int index in dirtyProperties)
      {
        var msg = string.Format(
          "Dirty property {0}.{1} was {2}, is {3}.",
          className,
          persister.PropertyNames[index],
          oldState[index] ?? "null",
          currentState[index] ?? "null");
        log.Error(msg);
        _ghosts.Add(msg);
      }

    }

  }
}
