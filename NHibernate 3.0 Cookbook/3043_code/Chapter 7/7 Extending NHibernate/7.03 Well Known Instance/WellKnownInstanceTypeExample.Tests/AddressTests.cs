using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLiteTesting;
using NUnit.Framework;
using FluentNHibernate.Testing;

namespace WKITExample.Tests
{

  [TestFixture]
  public class AddressTests : NHibernateFixture 
  {

    [Test]
    public void PersistenceTest()
    {

      new PersistenceSpecification<Address>(Session)
      .CheckProperty(p => p.Line1, "123 Anywhere")
      .CheckProperty(p => p.City, "Houston")
      .CheckProperty(p => p.State, States.Texas)
      .CheckProperty(p => p.Zip, "77090")
      .VerifyTheMappings();

    }

  }

}
