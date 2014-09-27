using NHibernate;

namespace SessionPerPresenter.Data
{
  class TransactionProviderImpl : ITransactionProvider 
  {

    private readonly ISessionProvider _sessionProvider;

    public TransactionProviderImpl(
      ISessionProvider sessionProvider)
    {
      _sessionProvider = sessionProvider;
    }

    public ITransaction BeginTransaction()
    {
      var session = _sessionProvider.GetCurrentSession();
      return session.BeginTransaction();
    }

    public void Dispose()
    {
      _sessionProvider.Dispose();
    }

  }
}
