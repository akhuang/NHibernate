using System;
using NHibernate.Cfg;

namespace SysCache
{
  class Program
  {
    static void Main(string[] args)
    {

      var nhConfig = new Configuration().Configure();
      var sessionFactory = nhConfig.BuildSessionFactory();

    }

  }
}
