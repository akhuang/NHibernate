using NHibernate;

namespace SessionPerPresenter.Data
{
  public class DaoImpl<TEntity> : IDao<TEntity>
  {

    public readonly ISessionProvider _sessionProvider;

    public DaoImpl(ISessionProvider sessionProvider)
    {
      _sessionProvider = sessionProvider;
    }

    public void Dispose()
    {
      _sessionProvider.Dispose();
    }

    public void Save(TEntity entity)
    {
      var session = _sessionProvider.GetCurrentSession();
      try
      {
        session.SaveOrUpdate(entity);
      }
      catch (StaleObjectStateException)
      {
        _sessionProvider.ReplaceCurrentSession();
        throw;
      }
    }

  }
}
