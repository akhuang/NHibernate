using System;
using NHibernate.Bytecode;

namespace IoCByteCode.ByteCode
{

  public class ObjectsFactory : IObjectsFactory
  {

    public object CreateInstance(Type type, params object[] ctorArgs)
    {
      return Activator.CreateInstance(type, ctorArgs);
    }

    public object CreateInstance(Type type, bool nonPublic)
    {
      return Activator.CreateInstance(type, nonPublic);
    }

    public object CreateInstance(Type type)
    {
      return Activator.CreateInstance(type);
    }

  }

}
