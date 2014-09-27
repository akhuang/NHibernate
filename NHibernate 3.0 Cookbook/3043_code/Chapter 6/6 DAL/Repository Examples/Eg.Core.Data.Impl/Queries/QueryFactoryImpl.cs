using Eg.Core.Data.Queries;
using Microsoft.Practices.ServiceLocation;

namespace Eg.Core.Data.Impl.Queries
{
  public class QueryFactoryImpl : IQueryFactory 
  {

    private readonly IServiceLocator _serviceLocator;

    public QueryFactoryImpl(IServiceLocator serviceLocator)
    {
      _serviceLocator = serviceLocator;
    }

    #region IQueryFactory Members

    public TQuery CreateQuery<TQuery>() where TQuery : IQuery
    {
      return _serviceLocator.GetInstance<TQuery>();
    }

    #endregion
  }
}
