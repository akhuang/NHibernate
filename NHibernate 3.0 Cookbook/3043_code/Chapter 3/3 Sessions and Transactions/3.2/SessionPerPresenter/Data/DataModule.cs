using Ninject.Activation;
using Ninject.Modules;

namespace SessionPerPresenter.Data
{

  public class DataModule : NinjectModule 
  {

    public override void Load()
    {

      Kernel.Bind(typeof(IDao<>))
        .To(typeof(DaoImpl<>));

      Kernel.Bind<ITransactionProvider>()
        .To<TransactionProviderImpl>();

      Kernel.Bind<ISessionProvider>()
        .To<SessionProviderImpl>()
        .InScope(ctx => GetPresenterScope(ctx)); 
    }

    private static object GetPresenterScope(IContext context)
    {
      var request = context.Request;
      while (!IsPresenterRequestOrNull(request))
        request = request.ParentRequest;
      return request ?? new object();
    }

    private static bool IsPresenterRequestOrNull(
      IRequest request)
    {
      if (null == request)
        return true;
      return typeof(IPresenter)
        .IsAssignableFrom(request.Service);
    }

  }

}
