using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.NinjectAdapter;
using Ninject;
using System.Security.Cryptography;

namespace IoCByteCode.Tests
{

  public class CommonServiceLocatorConfiguration
  {

    public void Configure()
    {
      var kernel = BuildKernel();
      var sl = new NinjectServiceLocator(kernel);
      ServiceLocator.SetLocatorProvider(() => sl);
    }

    private IKernel BuildKernel()
    {
      var kernel = new StandardKernel();

      kernel.Bind<NHibernate.Proxy.IProxyFactory>()
        .To<NHibernate.ByteCode.Castle.ProxyFactory>()
        .InSingletonScope();

      kernel.Bind<IPasswordHasher>()
        .To<PasswordHasher>()
        .InSingletonScope();

      kernel.Bind<HashAlgorithm>()
        .To<MD5CryptoServiceProvider>()
        .InSingletonScope();

      return kernel;
    }

  }

}
