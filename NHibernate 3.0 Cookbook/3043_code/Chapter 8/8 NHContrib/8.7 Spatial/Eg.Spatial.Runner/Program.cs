using GisSharpBlog.NetTopologySuite.Geometries;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using log4net;
using log4net.Config;
using NHibernate.Cfg;
using NHibernate.Spatial.Criterion;
using NHibernate.Spatial.Mapping;

namespace Eg.Spatial.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      
      XmlConfigurator.Configure();
      var log = LogManager.GetLogger(typeof (Program));

      NHibernateProfiler.Initialize();

      var cfg = new Configuration().Configure();
      
      cfg.AddAuxiliaryDatabaseObject(
        new SpatialAuxiliaryDatabaseObject(cfg));

      var sessionFactory = cfg.BuildSessionFactory();


      //Houston, TX 
      var houstonTX = new Point(-95.383056, 29.762778);

      using (var session = sessionFactory.OpenSession())
      {
        using (var tx = session.BeginTransaction())
        {
          var query = session.CreateCriteria(
            typeof (StatePart))
            .Add(SpatialExpression.Contains(
              "Geometry", houstonTX));
          var part = query.UniqueResult<StatePart>();
          if (part == null)
          {
            log.InfoFormat("Houston, we have a problem.");
          }
          else
          {
            log.InfoFormat("Houston is in {0}", 
              part.Name);
          }
          tx.Commit();
        }
      }

    }
  }
}
