using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using log4net;

namespace SQLitePreloading
{
  public class SQLiteLoader
  {

    private static ILog log = LogManager.GetLogger(typeof(SQLiteLoader));

    private const string ATTACHED_DB = "asdfgaqwernb";

    public void ImportData(
      SQLiteConnection conn,
      string sourceDataFile)
    {
      
      var tables = GetTableNames(conn);
      AttachDatabase(conn, sourceDataFile);

      foreach (var table in tables)
      {
        var sourceTable = string.Format("{0}.{1}", 
          ATTACHED_DB, table);

        CopyTableData(conn, sourceTable, table);
      }

      DetachDatabase(conn);
    }

    public void ExportData(
      SQLiteConnection conn,
      string destinationDataFile)
    {
      var tables = GetTableNames(conn);
      AttachDatabase(conn, destinationDataFile);

      foreach (var table in tables)
      {
        var destTable = string.Format("{0}.{1}",
          ATTACHED_DB, table);

        CopyTableData(conn, table, destTable);
      }
      DetachDatabase(conn);
    }


    private IEnumerable<string> GetTableNames(
      SQLiteConnection conn)
    {
      string tables = SQLiteMetaDataCollectionNames.Tables;
      DataTable dt = conn.GetSchema(tables);
      return from DataRow R in dt.Rows
             select (string)R["TABLE_NAME"];
    }

    private void AttachDatabase(
      SQLiteConnection conn,
      string sourceDataFile)
    {
      SQLiteCommand cmd = new SQLiteCommand(conn);
      cmd.CommandText = String.Format("ATTACH '{0}' AS {1}",
        sourceDataFile, ATTACHED_DB);
      log.Debug(cmd.CommandText);
      cmd.ExecuteNonQuery();
    }

    private void CopyTableData(
      SQLiteConnection conn,
      string source,
      string destination)
    {
      SQLiteCommand cmd = new SQLiteCommand(conn);
      cmd.CommandText = string.Format(
        "INSERT INTO {0} SELECT * FROM {1}", 
        destination, source);
      log.Debug(cmd.CommandText);
      cmd.ExecuteNonQuery();
    }

    private void DetachDatabase(SQLiteConnection conn)
    {
      SQLiteCommand cmd = new SQLiteCommand(conn);
      cmd.CommandText = string.Format("DETACH {0}", ATTACHED_DB);
      log.Debug(cmd.CommandText);
      cmd.ExecuteNonQuery();
    }

  }
}
