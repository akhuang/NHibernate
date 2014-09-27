using SessionPerPresenter.Data;
using Eg.Core;

namespace SessionPerPresenter
{

  public class PresenterB : IPresenter 
  {

    public readonly IDao<Product> _productDao;
    public readonly ISessionProvider _sessionProvider;

    public PresenterB(IDao<Product> productDao,
      ISessionProvider sessionProvider)
    {
      _productDao = productDao;
      _sessionProvider = sessionProvider;
    }

    public virtual void Dispose()
    {
      _sessionProvider.Dispose();
    }

  }

}
