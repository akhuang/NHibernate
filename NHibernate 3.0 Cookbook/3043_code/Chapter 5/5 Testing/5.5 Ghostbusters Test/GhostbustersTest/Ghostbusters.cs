using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using NHibernate;
using NHibernate.Cfg;

namespace GhostbustersTest
{

  public class Ghostbusters
  {

    private static readonly ILog log = 
      LogManager.GetLogger(typeof(Ghostbusters));

    private readonly Configuration _configuration;
    private readonly ISessionFactory _sessionFactory;
    private readonly Action<string> _failCallback;
    private readonly Action<string> _inconclusiveCallback;

    public Ghostbusters(Configuration configuration,
    ISessionFactory sessionFactory,
    Action<string> failCallback,
    Action<string> inconclusiveCallback)
    {
      _configuration = configuration;
      _sessionFactory = sessionFactory;
      _failCallback = failCallback;
      _inconclusiveCallback = inconclusiveCallback;
    }

    public void Test()
    {
      var mappedEntityNames = _configuration.ClassMappings
      .Select(mapping => mapping.EntityName);

      foreach (string entityName in mappedEntityNames)
        Test(entityName);
    }

    public void Test<TEntity>()
    {
      Test(typeof(TEntity).FullName);
    }

    public void Test(string entityName)
    {
      object id = FindEntityId(entityName);
      if (id == null)
      {
        var msg = string.Format("No instances of {0} in database.", 
          entityName);
        _inconclusiveCallback.Invoke(msg);
        return;
      }
      log.DebugFormat("Testing entity {0} with id {1}", 
        entityName, id);
      Test(entityName, id);
    }

    public void Test(string entityName, object id)
    {
      var ghosts = new List<String>();
      var interceptor = new GhostInterceptor(ghosts);

      using (var session = _sessionFactory.OpenSession(interceptor))
      using (var tx = session.BeginTransaction())
      {
        session.Get(entityName, id);
        session.Flush();
        tx.Rollback();
      }

      if (ghosts.Any())
        _failCallback.Invoke(string.Join("\n", ghosts.ToArray()));
    }

    private object FindEntityId(string entityName)
    {
      object id;
      using (var session = _sessionFactory.OpenSession())
      {
        var idQueryString = string.Format(
          "SELECT e.id FROM {0} e", 
          entityName);

        var idQuery = session.CreateQuery(idQueryString)
        .SetMaxResults(1);

        using (var tx = session.BeginTransaction())
        {
          id = idQuery.UniqueResult();
          tx.Commit();
        }
      }
      return id;
    }

  }

}
