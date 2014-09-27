using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Eg.Core.Data.Impl.Queries
{

  public abstract class NamedQueryBase<TResult>
    : NHibernateQueryBase<TResult>, INamedQuery
  {

    public NamedQueryBase(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public override TResult Execute()
    {
      var nhQuery = GetNamedQuery();
      return Transact(() => Execute(nhQuery));
    }

    protected virtual string QueryName
    {
      get
      {
        return GetType().Name;
      }
    }

    protected virtual IDictionary<string, object> Parameters
    {
      get
      {
        return new Dictionary<string, object>();
      }
    }

    protected abstract TResult Execute(NHibernate.IQuery query);

    protected virtual NHibernate.IQuery GetNamedQuery()
    {
      var nhQuery = session.GetNamedQuery(
        ((INamedQuery) this).QueryName);
      SetParameters(nhQuery);
      return nhQuery;
    }

    protected virtual void SetParameters(
      NHibernate.IQuery nhQuery)
    {
      foreach (var kv in ((INamedQuery) this).Parameters)
        nhQuery.SetParameter(kv.Key, kv.Value);
    }


    #region INamedQuery Members

    string INamedQuery.QueryName
    {
      get { return QueryName; }
    }

    IDictionary<string, object> INamedQuery.Parameters
    {
      get { return Parameters; }
    }

    #endregion
  }

}
