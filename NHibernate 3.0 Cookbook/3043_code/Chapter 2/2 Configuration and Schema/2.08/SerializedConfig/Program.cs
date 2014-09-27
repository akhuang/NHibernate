
namespace SerializedConfig
{
  class Program
  {
    static void Main(string[] args)
    {
      var nhConfig = new ConfigurationBuilder().Build();
      var sessionFactory = nhConfig.BuildSessionFactory();
    }
  }
}
