using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Bytecode;
using NHibernate.Proxy;

namespace IoCByteCode.ByteCode
{

  public class ProxyFactoryFactory : IProxyFactoryFactory
  {

    private readonly IServiceLocator _serviceLocator;

    public ProxyFactoryFactory()
      : this(ServiceLocator.Current)
    { }

    public ProxyFactoryFactory(IServiceLocator serviceLocator)
    {
      _serviceLocator = serviceLocator;
    }

    public IProxyFactory BuildProxyFactory()
    {
      return _serviceLocator.GetInstance<IProxyFactory>();
    }

    public bool IsInstrumented(Type entityClass)
    {
      return false;
    }

    public IProxyValidator ProxyValidator
    {
      get { return new ProxyTypeValidator(); }
    }

  }

}
