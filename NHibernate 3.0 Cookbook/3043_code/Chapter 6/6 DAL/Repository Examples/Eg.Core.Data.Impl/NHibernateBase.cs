using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Eg.Core.Data.Impl
{

  public abstract class NHibernateBase
  {

    protected readonly ISessionFactory _sessionFactory;

    protected virtual ISession session
    {
      get
      {
        return _sessionFactory.GetCurrentSession();
      }
    }

    public NHibernateBase(ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    #region Auto Transaction Wrapping

    protected virtual TResult Transact<TResult>(Func<TResult> func)
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

    protected virtual void Transact(Action action)
    {
      Transact<bool>(() =>
      {
        action.Invoke();
        return false;
      });
    }

    #endregion

  }

}
