using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace AutoTransaction
{

  public class DataAccessObject<T, TId>
    where T : Entity<T, TId>
  {

    private readonly ISessionFactory _sessionFactory;

    private ISession session
    {
      get
      {
        return _sessionFactory.GetCurrentSession();
      }
    }

    public DataAccessObject(ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    public T Get(TId id)
    {
      return Transact(() => session.Get<T>(id));
    }

    public T Load(TId id)
    {
      return Transact(() => session.Load<T>(id));
    }

    public void Save(T entity)
    {
      Transact(() => session.SaveOrUpdate(entity));
    }

    public void Delete(T entity)
    {
      Transact(() => session.Delete(entity));
    }

    private TResult Transact<TResult>(Func<TResult> func)
    {
      if (!session.Transaction.IsActive)
      {
        // Wrap in transaction
        TResult result;
        using (var tx = session.BeginTransaction())
        {
          result = func.Invoke();
          tx.Commit();
        }
        return result;
      }

      // Don't wrap;
      return func.Invoke();
    }

    private void Transact(Action action)
    {
      Transact<bool>(() =>
      {
        action.Invoke();
        return false;
      });
    }

  }

  public class DataAccessObject<T>
    : DataAccessObject<T, Guid>
    where T : Entity<T>
  {
  }

}
