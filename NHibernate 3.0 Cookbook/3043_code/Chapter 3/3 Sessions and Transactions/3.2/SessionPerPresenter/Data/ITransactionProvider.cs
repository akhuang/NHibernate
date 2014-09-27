using System;
using NHibernate;

namespace SessionPerPresenter.Data
{
  public interface ITransactionProvider : IDisposable
  {
    ITransaction BeginTransaction();
  }
}
