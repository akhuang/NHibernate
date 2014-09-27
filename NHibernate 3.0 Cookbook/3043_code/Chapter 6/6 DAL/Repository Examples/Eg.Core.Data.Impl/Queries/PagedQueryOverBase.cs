using Eg.Core.Data.Queries;
using NHibernate;

namespace Eg.Core.Data.Impl.Queries
{
  public abstract class PagedQueryOverBase<T>
    : NHibernateQueryBase<PagedResult<T>>,
      IPagedQuery<T>
  {

    public PagedQueryOverBase(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public int PageNumber { get; set; }
    public int ItemsPerPage { get; set; }

    public override PagedResult<T> Execute()
    {
      var query = GetQuery();
      SetPaging(query);
      return Transact(() => Execute(query));
    }

    protected abstract IQueryOver<T, T> GetQuery();

    protected virtual void SetPaging(
      IQueryOver<T, T> query)
    {
      int maxResults = ItemsPerPage;
      int firstResult = (PageNumber - 1) * ItemsPerPage;
      query.Skip(firstResult).Take(maxResults);
    }

    protected virtual PagedResult<T> Execute(
      IQueryOver<T, T> query)
    {
      var results = query.Future<T>();
      var count = query.ToRowCountQuery().FutureValue<int>();
      return new PagedResult<T>()
      {
        PageOfResults = results,
        TotalItems = count.Value
      };
    }

  }
}
