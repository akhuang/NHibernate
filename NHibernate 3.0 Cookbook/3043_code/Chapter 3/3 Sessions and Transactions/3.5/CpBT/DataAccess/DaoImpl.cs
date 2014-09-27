using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace CpBT.DataAccess
{
  public class DaoImpl<TEntity> : IDao<TEntity>
  {

    private readonly ISessionFactory _sessionFactory;

    public DaoImpl(ISessionFactory sessionFactory)
    {
      _sessionFactory = sessionFactory;
    }

    protected ISession Session
    {
      get { return _sessionFactory.GetCurrentSession(); }
    }

    public TEntity Get(Guid Id)
    {
      return Session.Get<TEntity>(Id);
    }

    public void Save(TEntity entity)
    {
      Session.SaveOrUpdate(entity);
    }

  }
}
