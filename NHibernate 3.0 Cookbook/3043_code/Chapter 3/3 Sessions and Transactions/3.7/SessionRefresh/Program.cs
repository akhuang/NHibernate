using System;
using Eg.Core;
using NHibernate.Cfg;

namespace SessionRefresh
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

      var sessionA = sessionFactory.OpenSession();
      var sessionB = sessionFactory.OpenSession();

      Guid productId;
      Product productA;
      Product productB;

      productA = new Product()
      {
        Name = "Lawn Chair",
        Description = "Lime Green, Comfortable",
        UnitPrice = 10.00M
      };

      using (var tx = sessionA.BeginTransaction())
      {
        Console.WriteLine("Saving product.");
        productId = (Guid) sessionA.Save(productA);
        tx.Commit();
      }

      using (var tx = sessionB.BeginTransaction())
      {
        Console.WriteLine("Changing price.");
        productB = sessionB.Get<Product>(productId);
        productB.UnitPrice = 15.00M;
        tx.Commit();
      }

      using (var tx = sessionA.BeginTransaction())
      {
        Console.WriteLine("Price was {0:c}",
          productA.UnitPrice);

        sessionA.Refresh(productA);

        Console.WriteLine("Price is {0:c}", 
          productA.UnitPrice);
        tx.Commit();
      }

      sessionA.Close();
      sessionB.Close();

      Console.ReadKey();

    }

  }
}
