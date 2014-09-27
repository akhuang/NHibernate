
namespace Eg.Core.Data.Queries
{

  public interface IQueryFactory
  {

    TQuery CreateQuery<TQuery>() where TQuery : IQuery;

  }

}
