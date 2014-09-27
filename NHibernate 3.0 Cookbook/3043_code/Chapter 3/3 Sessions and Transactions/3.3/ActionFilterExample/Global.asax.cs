using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;

namespace ActionFilterExample
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {

    public static ISessionFactory SessionFactory { get; private set; }

    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          "Default",                                              // Route name
          "{controller}/{action}/{id}",                           // URL with parameters
          new { controller = "Book", action = "Index", id = "" }  // Parameter defaults
      );

    }

    protected void Application_Start()
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      SessionFactory = nhConfig.BuildSessionFactory();

      RegisterRoutes(RouteTable.Routes);
    }
  }
}