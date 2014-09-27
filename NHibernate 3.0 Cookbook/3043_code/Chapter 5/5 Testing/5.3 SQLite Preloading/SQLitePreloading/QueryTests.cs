using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using System.Data.SQLite;
using NUnit.Framework;
using Eg.Core;

namespace SQLitePreloading
{
  public class QueryTests : DataDependentFixture  
  {

    protected override string GetSQLiteFilename()
    {
      return @"L:\testData.db3";
    }

    [Test]
    public void Director_query_should_return_one_movie()
    {

      var query = Session.QueryOver<Movie>()
        .Where(m => m.Director == "Tim Burton");

      using (var tx = Session.BeginTransaction())
      {
        var movies = query.List<Movie>();
        Assert.That(movies.Count == 1);
        tx.Commit();
      }
    }

    [Test]
    public void Director_query_should_return_empty()
    {
      var query = Session.QueryOver<Movie>()
        .Where(m => m.Director == "Jason Dentler");

      using (var tx = Session.BeginTransaction())
      {
        var movies = query.List<Movie>();
        Assert.That(movies.Count == 0);
        tx.Commit();
      }
    }

  }
}
