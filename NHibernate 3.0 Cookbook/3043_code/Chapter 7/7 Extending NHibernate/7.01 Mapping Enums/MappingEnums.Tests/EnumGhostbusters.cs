using System;
using GhostbustersTest;
using NUnit.Framework;

namespace MappingEnums.Tests
{

  [TestFixture]
  public class EnumGhostbusters : NHibernateFixture
  {

    [Test]
    public void GhostbustersTest()
    {

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Account()
        {
          Name = "Jason Dentler",
          Number = "123456",
          AcctType = AccountTypes.Corporate
        });

        tx.Commit();
      }

      new Ghostbusters(
        NHConfigurator.Configuration,
        NHConfigurator.SessionFactory,
        new Action<string>(msg => Assert.Fail(msg)),
        new Action<string>(msg => Assert.Inconclusive(msg))
      ).Test();

    }

  }
}
