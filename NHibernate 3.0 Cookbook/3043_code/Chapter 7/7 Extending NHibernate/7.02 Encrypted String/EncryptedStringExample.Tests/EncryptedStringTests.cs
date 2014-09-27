using System;
using NUnit.Framework;
using SQLiteTesting;

namespace EncryptedStringExample.Tests
{
  [TestFixture]
  public class EncryptedStringTests : NHibernateFixture 
  {

    [Test]
    public void EncryptionRoundTrip()
    {
      string data = "Hello World!";
      string plainTextData = string.Copy(data);
      string encryptedData;

      var encryptor = new DESEncryptor()
                        {
                          EncryptionKey = "12345678"
                        };

      encryptedData = encryptor.Encrypt(plainTextData);
      plainTextData = encryptor.Decrypt(encryptedData);

      Assert.AreEqual(data, plainTextData);
      Assert.AreNotEqual(data, encryptedData);

    }

    [Test]
    public void AccountRoundTrip()
    {
      string cardNumber = "4111-1111-1111-1111";
      var account = new Account()
      {
        Name = "John Smith",
        EMail = "test@test.edu",
        CardNumber = cardNumber,
        ExpirationMonth = 2,
        ExpirationYear = 2013,
        ZipCode = "55555"
      };
      Guid accountId;

      using (var tx = Session.BeginTransaction())
      {
        accountId = (Guid) Session.Save(account);
        tx.Commit();
      };

      Session.Clear();

      using (var tx = Session.BeginTransaction())
      {
        account = Session.Get<Account>(accountId);
        tx.Commit();
      }

      Assert.AreEqual(cardNumber, account.CardNumber);

    }

  }

}
