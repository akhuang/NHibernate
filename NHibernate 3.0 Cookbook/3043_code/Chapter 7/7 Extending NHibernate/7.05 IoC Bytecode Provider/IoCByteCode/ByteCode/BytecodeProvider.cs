using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Bytecode;
using NHibernate.Properties;
using NHibernate.Type;

namespace IoCByteCode.ByteCode
{

  public class BytecodeProvider 
    : IBytecodeProvider, 
    IInjectableCollectionTypeFactoryClass 
  {

    private readonly IServiceLocator _serviceLocator;
    private readonly IObjectsFactory _objectsFactory;
    private ICollectionTypeFactory _collectionTypeFactory;

    public BytecodeProvider()
      : this(ServiceLocator.Current)
    {
    }

    public BytecodeProvider(IServiceLocator serviceLocator)
    {
      _serviceLocator = serviceLocator;
      _objectsFactory = new ObjectsFactory();
      _collectionTypeFactory = new DefaultCollectionTypeFactory();
    }


    public ICollectionTypeFactory CollectionTypeFactory
    {
      get { return _collectionTypeFactory; }
    }

    public IReflectionOptimizer GetReflectionOptimizer(
      Type clazz, IGetter[] getters, ISetter[] setters)
    {
      return new ReflectionOptimizer(
        _serviceLocator, clazz, getters, setters);
    }

    public IObjectsFactory ObjectsFactory
    {
      get { return _objectsFactory; }
    }

    public IProxyFactoryFactory ProxyFactoryFactory
    {
      get { return new ProxyFactoryFactory(_serviceLocator); }
    }

    public void SetCollectionTypeFactoryClass(Type type)
    {
      _collectionTypeFactory = (ICollectionTypeFactory)
        Activator.CreateInstance(type);
    }

    public void SetCollectionTypeFactoryClass(
      string typeAssemblyQualifiedName)
    {
      var type = Type.GetType(typeAssemblyQualifiedName);
      SetCollectionTypeFactoryClass(type);
    }

  }


}
