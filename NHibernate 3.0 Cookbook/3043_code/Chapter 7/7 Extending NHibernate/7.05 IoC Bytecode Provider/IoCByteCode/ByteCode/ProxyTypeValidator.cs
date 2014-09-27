using System;
using NHibernate.Proxy;

namespace IoCByteCode.ByteCode
{
  public class ProxyTypeValidator : DynProxyTypeValidator 
  {

    protected override void CheckHasVisibleDefaultConstructor(Type type)
    {
    }

  }
}
