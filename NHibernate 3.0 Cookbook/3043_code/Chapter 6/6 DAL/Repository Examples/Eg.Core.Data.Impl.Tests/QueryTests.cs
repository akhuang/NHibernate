using System;
using System.Linq;
using Eg.Core.Data.Impl.Queries;
using NUnit.Framework;
using SQLiteTesting;

namespace Eg.Core.Data.Impl.Tests
{

  [TestFixture]
  public class QueryTests : NHibernateFixture 
  {

    [Test]
    public void NamedQueryParametersCheck()
    {

      var namedQueryType = typeof(INamedQuery);
      var queryImplAssembly = typeof(BookWithISBN).Assembly;

      var types = from t in queryImplAssembly.GetTypes()
                  where namedQueryType.IsAssignableFrom(t)
                  && t.IsClass 
                  && !t.IsAbstract
                  select t;

      var nhCfg = NHConfigurator.Configuration;

      var mappedQueries = nhCfg.NamedQueries.Keys
        .Union(nhCfg.NamedSQLQueries.Keys);

      var errors = new System.Text.StringBuilder();

      foreach (var queryType in types)
      {
        var query = (INamedQuery)
          Activator.CreateInstance(queryType, new object[] { SessionFactory });

        if (!mappedQueries.Contains(query.QueryName))
        {
          errors.AppendFormat(
            "Query object {0} references non-existent named query {1}.",
            queryType, query.QueryName);

          errors.AppendLine();
          continue;
        }

        var nhQuery = Session.GetNamedQuery(query.QueryName);

        var mapParams = nhQuery.NamedParameters;
        var objParams = query.Parameters.Keys;

        foreach (var paramName in mapParams.Except(objParams))
        {
          errors.AppendFormat("{0} has param {1} in mapping, but not object",
            query.QueryName, paramName);
          errors.AppendLine();
        }

        foreach (var paramName in objParams.Except(mapParams))
        {
          errors.AppendFormat("{0} has param {1} in object, but not mapping",
            query.QueryName, paramName);
          errors.AppendLine();
        }

      }

      if (errors.Length != 0)
        Assert.Fail(errors.ToString());

    }


  }

}
