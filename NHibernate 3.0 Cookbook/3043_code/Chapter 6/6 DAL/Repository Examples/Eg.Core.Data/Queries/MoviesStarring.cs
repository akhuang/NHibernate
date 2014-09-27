using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqSpecs;

namespace Eg.Core.Data.Queries
{

  public class MoviesStarring : Specification<Movie>
  {

    private readonly string _actor;

    public MoviesStarring(string actor)
    {
      _actor = actor;
    }

    public override Expression<Func<Movie, bool>> IsSatisfiedBy()
    {
      return m => m.Actors.Any(a => a.Actor == _actor);
    }

  }

}
