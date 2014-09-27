using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Properties;

namespace IoCByteCode.ByteCode
{

  public class ReflectionOptimizer 
    : NHibernate.Bytecode.Lightweight.ReflectionOptimizer 
  {

    protected readonly IServiceLocator _serviceLocator;

    public ReflectionOptimizer(IServiceLocator serviceLocator,
    Type mappedType, IGetter[] getters, ISetter[] setters)
      : base(mappedType, getters, setters)
    {
      _serviceLocator = serviceLocator;
    }

    protected override void ThrowExceptionForNoDefaultCtor(Type type)
    {
    }

    public override object CreateInstance()
    {
      return _serviceLocator.GetInstance(mappedType);
    }

  }

}
