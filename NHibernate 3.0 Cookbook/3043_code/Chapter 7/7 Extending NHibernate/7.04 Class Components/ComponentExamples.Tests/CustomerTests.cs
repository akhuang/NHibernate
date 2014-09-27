using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentNHibernate.Testing;
using SQLiteTesting;

namespace ComponentExamples.Tests
{
  [TestFixture]
  public class CustomerTests : NHibernateFixture
  {

    [Test]
    public void PersistenceTests()
    {

      var expected = new Customer()
      {
        Name = "Joey Smith", 
        BillingAddress = new Address() {
           Lines = "123 Billing Lane",
           City = "Billsville",
           State = "TX",
           ZipCode = "12345"
        },
        ShippingAddress = new Address() {
          Lines = "123 Shipping Lane",
          City = "Shipsdale",
          State = "AZ",
          ZipCode = "54321"
        }
      };
      Customer actual;
      Guid customerId;

      using (var tx = Session.BeginTransaction())
      {
        customerId = (Guid)Session.Save(expected);
        tx.Commit();
      }

      Session.Clear();

      using (var tx = Session.BeginTransaction())
      {
        actual = Session.Get<Customer>(customerId);
        tx.Commit();
      }

      Assert.AreEqual(expected.Name, actual.Name);
      Assert.AreEqual(expected.BillingAddress.Lines, actual.BillingAddress.Lines);
      Assert.AreEqual(expected.BillingAddress.City, actual.BillingAddress.City);
      Assert.AreEqual(expected.BillingAddress.State, actual.BillingAddress.State);
      Assert.AreEqual(expected.BillingAddress.ZipCode, actual.BillingAddress.ZipCode);
      Assert.AreEqual(expected.ShippingAddress.Lines, actual.ShippingAddress.Lines);
      Assert.AreEqual(expected.ShippingAddress.City, actual.ShippingAddress.City);
      Assert.AreEqual(expected.ShippingAddress.State, actual.ShippingAddress.State);
      Assert.AreEqual(expected.ShippingAddress.ZipCode, actual.ShippingAddress.ZipCode);

    }

  }
}
