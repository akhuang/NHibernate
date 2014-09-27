using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace SessionPerRequestGlobal
{
  public class Global : System.Web.HttpApplication
  {

    public static ISessionFactory SessionFactory { get; private set; }

    protected void Application_Start(object sender, EventArgs e)
    {
      log4net.Config.XmlConfigurator.Configure();
      var nhConfig = new Configuration().Configure();
      SessionFactory = nhConfig.BuildSessionFactory();
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      var session = SessionFactory.OpenSession();
      CurrentSessionContext.Bind(session);
    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {
      var session = CurrentSessionContext.Unbind(SessionFactory);
      session.Dispose();
    }

  }
}