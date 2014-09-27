using System.Collections.Generic;
using Eg.Core.Data.Queries;
using LinqSpecs;

namespace Eg.Core.Data
{

  public interface IRepository<T> : ICollection<T>, IQueryFactory
    where T : Entity 
  {

    IEnumerable<T> FindAll(Specification<T> specification);
    T FindOne(Specification<T> specification);

  }

}
