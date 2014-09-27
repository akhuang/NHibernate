using System.Data;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Connection;
using System;

namespace ContextInfoExample
{

  public class ContextInfoConnectionDriver : 
    DriverConnectionProvider 
  {

    private const string COMMAND_TEXT = 
      "declare @length tinyint\n" +
      "declare @ctx varbinary(128)\n" +
      "select @length = len(@data)\n" +
      "select @ctx = convert(binary(1), @length) + " +
      "convert(binary(127), @data)\n" +
      "set context_info @ctx";


    public override IDbConnection GetConnection()
    {
      var conn = base.GetConnection();
      SetContext(conn);
      return conn;
    }

    public override void CloseConnection(IDbConnection conn)
    {
      EraseContext(conn);
      base.CloseConnection(conn);
    }

    private void SetContext(IDbConnection conn)
    {
      var sl = ServiceLocator.Current;
      var dataProvider = sl.GetInstance<IContextDataProvider>();
      var data = dataProvider.GetData();
      SetContext(conn, data);
    }

    private void EraseContext(IDbConnection conn)
    {
      var sl = ServiceLocator.Current;
      var dataProvider = sl.GetInstance<IContextDataProvider>();
      var data = dataProvider.GetEmptyData();
      SetContext(conn, data);
    }

    private void SetContext(IDbConnection conn, string data)
    {
      var cmd = conn.CreateCommand();
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = COMMAND_TEXT;

      var param = cmd.CreateParameter();
      param.ParameterName = "@data";
      param.DbType = DbType.AnsiString;
      param.Size = 127;
      param.Value = data;
      cmd.Parameters.Add(param);

      cmd.ExecuteNonQuery();
    }

  }

}
