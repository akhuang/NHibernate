using System;
using System.Data;
using NHibernate.Connection;
using log4net;

namespace GhostbustersTest
{
  public class TestConnectionProvider : DriverConnectionProvider
  {

    static ILog log = LogManager.GetLogger(typeof(TestConnectionProvider));

    [ThreadStatic]
    private static IDbConnection Connection;

    public static void CloseDatabase()
    {
      log.Debug("Closing database.");
      if (Connection != null)
        Connection.Dispose();
      Connection = null;
    }

    public override IDbConnection GetConnection()
    {
      if (Connection == null)
      {
        log.Debug("Creating database.");
        Connection = Driver.CreateConnection();
        Connection.ConnectionString = ConnectionString;
        Connection.Open();
      }
      return Connection;
    }

    public override void CloseConnection(IDbConnection conn)
    {
      // Do nothing
    }

  }
}
