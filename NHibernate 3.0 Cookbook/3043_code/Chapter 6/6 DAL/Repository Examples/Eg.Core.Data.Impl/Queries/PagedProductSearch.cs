using System.Collections.Generic;
using Eg.Core.Data.Queries;
using NHibernate;
using NHibernate.Criterion;

namespace Eg.Core.Data.Impl.Queries
{

  public class PagedProductSearch
    : PagedQueryOverBase<Product>, 
      IPagedProductSearch  
  {

    public PagedProductSearch(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }
    public PagedProductSearchSort
      Sort { get; set; }

    protected override IQueryOver<Product, Product> GetQuery()
    {
      var query = session.QueryOver<Product>();
      if (!string.IsNullOrEmpty(Name))
        query = query.WhereRestrictionOn(p => p.Name)
          .IsInsensitiveLike(Name, MatchMode.Anywhere);

      if (!string.IsNullOrEmpty(Description))
        query.WhereRestrictionOn(p => p.Description)
          .IsInsensitiveLike(Description, MatchMode.Anywhere);

      if (MinimumPrice.HasValue)
        query.Where(p => p.UnitPrice >= MinimumPrice);

      if (MaximumPrice.HasValue)
        query.Where(p => p.UnitPrice <= MaximumPrice);

      switch (Sort)
      {
        case PagedProductSearchSort.PriceDesc:
          query = query.OrderBy(p => p.UnitPrice).Desc;
          break;
        case PagedProductSearchSort.Name:
          query = query.OrderBy(p => p.Name).Asc;
          break;
        default:
          query = query.OrderBy(p => p.UnitPrice).Asc;
          break;
      }
      return query;
    }



  }

}
