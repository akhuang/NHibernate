using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Metadata;
using NHibernate.Stat;

namespace Eg.Search.Runner
{

  public class SessionFactorySearchWrapper 
    : ISessionFactory
  {
    private readonly ISessionFactory _sessionFactory;

    public SessionFactorySearchWrapper(
      ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    #region Delegate to _sessionFactory

    public void Close()
    {
      _sessionFactory.Close();
    }

    public ICollection<string> DefinedFilterNames
    {
      get { return _sessionFactory.DefinedFilterNames; }
    }

    public void Evict(Type persistentClass, object id)
    {
      _sessionFactory.Evict(persistentClass, id);
    }

    public void Evict(Type persistentClass)
    {
      _sessionFactory.Evict(persistentClass);
    }

    public void EvictCollection(string roleName, object id)
    {
      _sessionFactory.EvictCollection(roleName, id);
    }

    public void EvictCollection(string roleName)
    {
      _sessionFactory.EvictCollection(roleName);
    }

    public void EvictEntity(string entityName, object id)
    {
      _sessionFactory.EvictEntity(entityName, id);
    }

    public void EvictEntity(string entityName)
    {
      _sessionFactory.EvictEntity(entityName);
    }

    public void EvictQueries(string cacheRegion)
    {
      _sessionFactory.EvictQueries(cacheRegion);
    }

    public void EvictQueries()
    {
      _sessionFactory.EvictQueries();
    }

    public IDictionary<string, IClassMetadata> GetAllClassMetadata()
    {
      return _sessionFactory.GetAllClassMetadata();
    }

    public IDictionary<string, ICollectionMetadata> GetAllCollectionMetadata()
    {
      return _sessionFactory.GetAllCollectionMetadata();
    }

    public IClassMetadata GetClassMetadata(string entityName)
    {
      return _sessionFactory.GetClassMetadata(entityName);
    }

    public IClassMetadata GetClassMetadata(Type persistentClass)
    {
      return _sessionFactory.GetClassMetadata(persistentClass);
    }

    public ICollectionMetadata GetCollectionMetadata(string roleName)
    {
      return _sessionFactory.GetCollectionMetadata(roleName);
    }

    public ISession GetCurrentSession()
    {
      return _sessionFactory.GetCurrentSession();
    }

    public FilterDefinition GetFilterDefinition(string filterName)
    {
      return _sessionFactory.GetFilterDefinition(filterName);
    }

    public bool IsClosed
    {
      get { return _sessionFactory.IsClosed; }
    }

    public IStatelessSession OpenStatelessSession(
      IDbConnection connection)
    {
      return _sessionFactory.OpenStatelessSession(connection);
    }

    public IStatelessSession OpenStatelessSession()
    {
      return _sessionFactory.OpenStatelessSession();
    }

    public IStatistics Statistics
    {
      get { return _sessionFactory.Statistics; }
    }

    public void Dispose()
    {
      _sessionFactory.Dispose();
    }

    #endregion

    public ISession OpenSession()
    {
      var session = _sessionFactory.OpenSession();
      return WrapSession(session);
    }

    public ISession OpenSession(
      IDbConnection conn, 
      IInterceptor sessionLocalInterceptor)
    {
      var session = _sessionFactory
        .OpenSession(conn, sessionLocalInterceptor);
      return WrapSession(session);
    }

    public ISession OpenSession(
      IInterceptor sessionLocalInterceptor)
    {
      var session = _sessionFactory
        .OpenSession(sessionLocalInterceptor);
      return WrapSession(session);
    }

    public ISession OpenSession(
      IDbConnection conn)
    {
      var session = _sessionFactory.OpenSession(conn);
      return WrapSession(session);
    }

    private static ISession WrapSession(
      ISession session)
    {
      return NHibernate.Search
        .Search.CreateFullTextSession(session);
    }

  }
}
