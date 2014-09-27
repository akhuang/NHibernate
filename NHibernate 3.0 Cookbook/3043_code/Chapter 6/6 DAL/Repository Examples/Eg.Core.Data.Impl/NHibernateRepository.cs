using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using System.Collections;
using Eg.Core.Data.Queries;
using IQuery = Eg.Core.Data.Queries.IQuery;
using LinqSpecs;

namespace Eg.Core.Data.Impl
{
  public class NHibernateRepository<T> : NHibernateBase,
  IRepository<T> where T : Entity
  {

    private readonly IQueryFactory _queryFactory;

    public NHibernateRepository(ISessionFactory sessionFactory,
      IQueryFactory queryFactory)
      : base(sessionFactory)
    {
      _queryFactory = queryFactory;
    }


    #region ICollection<T> Members

    public void Add(T item)
    {
      Transact(() => session.Save(item));
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
      if (item.Id == default(Guid))
        return false;
      return Transact(() => session.Get<T>(item.Id)) != null;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return Transact(() => session.Query<T>().Count());
      }
    }

    public bool IsReadOnly
    {
      get { return false; }
    }

    public bool Remove(T item)
    {
      Transact(() => session.Delete(item));
      return true;
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      return Transact(() => session.Query<T>().Take(1000).GetEnumerator());
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Transact(() => GetEnumerator());
    }

    #endregion

    #region IQueryFactory Members

    public TQuery CreateQuery<TQuery>() where TQuery : IQuery
    {
      return _queryFactory.CreateQuery<TQuery>();
    }

    #endregion


    #region IRepository<T> Members

    public IEnumerable<T> FindAll(Specification<T> specification)
    {
      var query = GetQuery(specification);
      return Transact(() => query.ToList());
    }

    public T FindOne(Specification<T> specification)
    {
      var query = GetQuery(specification);
      return Transact(() => query.SingleOrDefault());
    }

    private IQueryable<T> GetQuery(
      Specification<T> specification)
    {
      return session.Query<T>()
        .Where(specification.IsSatisfiedBy());
    }

    #endregion
  }
}
