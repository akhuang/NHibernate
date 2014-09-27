using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eg.Core.Data.Queries;
using NHibernate;
using NHibernate.Criterion;

namespace Eg.Core.Data.Impl.Queries
{

  public abstract class CriteriaQueryBase<TResult> : 
    NHibernateQueryBase<TResult>, ICriteriaQuery 
  {

    public CriteriaQueryBase(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public override TResult Execute()
    {
      var criteria = GetCriteria();
      return Transact(() => Execute(criteria));
    }

    protected abstract ICriteria GetCriteria();

    protected abstract TResult Execute(ICriteria criteria);

  }

}
