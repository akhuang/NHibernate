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
  public class SalespersonTests : NHibernateFixture 
  {

    [Test]
    public void PersistenceTest()
    {

      new PersistenceSpecification<Salesperson>(Session)
      .CheckProperty(p => p.Name, "Joey Smith")
      .CheckProperty(p => p.Region, Regions.SouthWest)
      .VerifyTheMappings();

    }

  }

}
