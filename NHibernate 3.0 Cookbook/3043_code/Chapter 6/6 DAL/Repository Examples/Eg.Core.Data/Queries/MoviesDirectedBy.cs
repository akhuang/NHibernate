using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace Eg.Core.Data.Queries
{
  public class MoviesDirectedBy : Specification<Movie>
  {

    private readonly string _director;

    public MoviesDirectedBy(string director)
    {
      _director = director;
    }

    public override Expression<Func<Movie, bool>> IsSatisfiedBy()
    {
      return m => m.Director == _director;
    }

  }
}
