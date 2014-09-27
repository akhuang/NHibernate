using System;
using NHibernate.Cfg;

namespace ConfigByXML
{
  class Program
  {

    static void Main(string[] args)
    {

      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();
      Console.WriteLine("NHibernate Configured!");
      Console.ReadKey();

    }

  }

}
