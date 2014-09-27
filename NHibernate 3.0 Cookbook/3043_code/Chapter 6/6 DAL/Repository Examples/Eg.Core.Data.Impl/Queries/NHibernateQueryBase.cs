using Eg.Core.Data.Queries;
using NHibernate;

namespace Eg.Core.Data.Impl.Queries
{
  public abstract class NHibernateQueryBase<TResult> 
    : NHibernateBase, IQuery<TResult>
  {

    public NHibernateQueryBase(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public abstract TResult Execute();

  }
}
