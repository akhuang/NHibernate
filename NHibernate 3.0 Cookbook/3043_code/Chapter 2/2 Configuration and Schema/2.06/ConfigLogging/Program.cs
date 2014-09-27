using NHibernate.Cfg;

namespace ConfigLogging
{
    class Program
    {
        static void Main(string[] args)
        {

            log4net.Config.XmlConfigurator.Configure();
            var nhConfig = new Configuration().Configure();
            var sessionFactory = nhConfig.BuildSessionFactory();

        }
    }
}
