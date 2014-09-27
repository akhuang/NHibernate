using System.Collections.Generic;
using Eg.Core.Data.Queries;
using NHibernate;
using NHibernate.Criterion;

namespace Eg.Core.Data.Impl.Queries
{

  public class AdvancedProductSearch
    : CriteriaQueryBase<IEnumerable<Product>>, 
      IAdvancedProductSearch 
  {

    public AdvancedProductSearch(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public string Director { get; set; }
    public string Actor { get; set; }
    public AdvancedProductSearchSort
      Sort { get; set; }

    protected override ICriteria GetCriteria()
    {
      if (HasBookCriterion())
        return GetBookQuery().UnderlyingCriteria;
      if (HasMovieCriterion())
        return GetMovieQuery().UnderlyingCriteria;
      return GetProductQuery().UnderlyingCriteria;
    }

    protected override IEnumerable<Product> Execute(ICriteria criteria)
    {
      return criteria.List<Product>();
    }

    private bool HasBookCriterion()
    {
      return !string.IsNullOrEmpty(ISBN) ||
        !string.IsNullOrEmpty(Author);
    }

    private bool HasMovieCriterion()
    {
      return !string.IsNullOrEmpty(Director) ||
        !string.IsNullOrEmpty(Actor);
    }

    private IQueryOver GetProductQuery()
    {
      var query = session.QueryOver<Product>();
      AddProductCriterion(query);
      return query;
    }

    private void AddProductCriterion<T>(
      IQueryOver<T, T> query) where T : Product 
    {

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
        case AdvancedProductSearchSort.PriceDesc:
          query = query.OrderBy(p => p.UnitPrice).Desc;
          break;
        case AdvancedProductSearchSort.Name:
          query = query.OrderBy(p => p.Name).Asc;
          break;
        default:
          query = query.OrderBy(p => p.UnitPrice).Asc;
          break;
      }

    }

    private IQueryOver GetBookQuery()
    {
      var bookQuery = session.QueryOver<Book>();

      AddProductCriterion(bookQuery);

      if (!string.IsNullOrEmpty(ISBN))
        bookQuery.Where(b => b.ISBN == ISBN);

      if (!string.IsNullOrEmpty(Author))
        bookQuery.WhereRestrictionOn(b => b.Author)
          .IsInsensitiveLike(Author, MatchMode.Anywhere);

      return bookQuery;
    }

    private IQueryOver GetMovieQuery()
    {

      var movieQuery = session.QueryOver<Movie>();
      AddProductCriterion(movieQuery);

      if (!string.IsNullOrEmpty(Director))
        movieQuery.WhereRestrictionOn(m => m.Director)
          .IsInsensitiveLike(Director, MatchMode.Anywhere);

      if (!string.IsNullOrEmpty(Actor))
      {
        movieQuery
          .Inner.JoinQueryOver<ActorRole>(m => m.Actors)
          .WhereRestrictionOn(ar => ar.Actor)
          .IsInsensitiveLike(Actor, MatchMode.Anywhere);
      }

      return movieQuery;
    }


  }

}
