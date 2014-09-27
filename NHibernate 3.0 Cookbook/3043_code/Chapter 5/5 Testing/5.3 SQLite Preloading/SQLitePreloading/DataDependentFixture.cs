using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SQLitePreloading
{

  public abstract class DataDependentFixture : NHibernateFixture 
  {

    protected abstract string GetSQLiteFilename();

    protected override void OnSetup()
    {
      base.OnSetup();
      var conn = (SQLiteConnection) Session.Connection;
      new SQLiteLoader().ImportData(conn, GetSQLiteFilename());
    }

  }
}
