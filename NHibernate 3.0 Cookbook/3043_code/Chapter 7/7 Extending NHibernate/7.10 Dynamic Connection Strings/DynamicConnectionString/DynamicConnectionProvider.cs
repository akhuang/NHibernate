using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Connection;
using System.Security.Principal;
using System.Security;
using System.Configuration;

namespace DynamicConnectionString
{

  public class DynamicConnectionProvider : DriverConnectionProvider
  {

    private const string ANON_CONN_NAME = "db";
    private const string AUTH_CONN_NAME = "auth_db";

    protected override string ConnectionString
    {
      get
      {
        var connstrs = ConfigurationManager.ConnectionStrings;
        var connstr = connstrs[ANON_CONN_NAME];
        if (IsAuthenticated())
          connstr = connstrs[AUTH_CONN_NAME];
        return connstr.ConnectionString;
      }
    }

    private bool IsAuthenticated()
    {
      var identity = WindowsIdentity.GetCurrent();
      return identity != null && identity.IsAuthenticated;
    }

  }

}
