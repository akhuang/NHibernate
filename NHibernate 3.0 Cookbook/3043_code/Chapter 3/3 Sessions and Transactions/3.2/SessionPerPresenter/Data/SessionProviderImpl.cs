using System;
using NHibernate;

namespace SessionPerPresenter.Data
{

  public class SessionProviderImpl
    : ISessionProvider
  {

    private readonly ISessionFactory _sessionFactory;
    private ISession _currentSession;

    public SessionProviderImpl(ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    public ISession GetCurrentSession()
    {
      if (null == _currentSession)
        _currentSession = _sessionFactory.OpenSession();
      return _currentSession;
    }

    public void ReplaceCurrentSession()
    {
      _currentSession.Dispose();
      _currentSession = null;
    }

    public void Dispose()
    {
      if (_currentSession != null)
        _currentSession.Dispose();
      _currentSession = null;
    }

  }

}
