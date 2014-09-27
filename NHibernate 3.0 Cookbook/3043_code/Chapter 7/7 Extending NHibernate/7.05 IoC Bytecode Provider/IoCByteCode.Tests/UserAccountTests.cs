using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteTesting;
using NUnit.Framework;
using Microsoft.Practices.ServiceLocation;

namespace IoCByteCode.Tests
{

  [TestFixture]
  public class UserAccountTests : NHibernateFixture 
  {

    protected override void OnFixtureSetup()
    {
      new CommonServiceLocatorConfiguration().Configure();
      NHibernate.Cfg.Environment.BytecodeProvider =
        new IoCByteCode.ByteCode.BytecodeProvider();
      base.OnFixtureSetup();
    }

    [Test]
    public void Password_is_hashed()
    {
      string email = "email@domain.com";
      string password = "password";
      var serviceLocator = ServiceLocator.Current;
      var account = serviceLocator.GetInstance<UserAccount>();
      account.SetCredentials(email, password);
      Assert.AreNotEqual(password, account.HashedPassword);
    }

    [Test]
    public void PersistenceTest()
    {
      string email = "email@domain.com";
      string hashedPassword;
      Guid accountId; 

      var serviceLocator = ServiceLocator.Current;
      var account = serviceLocator.GetInstance<UserAccount>();
      account.SetCredentials(email, "password");
      hashedPassword = account.HashedPassword;

      using (var session = SessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          accountId = (Guid)session.Save(account);
          tx.Commit();
        }
      }

      using (var session = SessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          account = session.Get<UserAccount>(accountId);
          tx.Commit();
        }
      }

      Assert.AreEqual(email, account.EMail);
      Assert.AreEqual(hashedPassword, account.HashedPassword);

    }

  }

}
