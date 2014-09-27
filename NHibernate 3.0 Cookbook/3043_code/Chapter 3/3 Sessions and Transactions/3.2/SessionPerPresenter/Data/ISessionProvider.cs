using System;
using NHibernate;

namespace SessionPerPresenter.Data
{
  public interface ISessionProvider : IDisposable
  {

    ISession GetCurrentSession();
    void ReplaceCurrentSession();

  }
}
