using System.IO;
using log4net;
namespace MyApp.MyNamespace
{

    public class Foo
    {

        private static ILog log = LogManager.GetLogger(typeof(Foo));

        public string DoSomething()
        {
            log.Debug("We're doing something.");
            try
            {
                return File.ReadAllText("cheese.txt");
            }
            catch (FileNotFoundException)
            {
                log.Error("Someone moved my cheese.txt");
                return string.Empty;
            }
        }
    }
}
