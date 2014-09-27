using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace ActionFilterExample
{
  [AttributeUsage(AttributeTargets.Method,
    AllowMultiple=false)]
  public class NHibernateSessionAttribute 
    : ActionFilterAttribute 
  {

    protected ISessionFactory sessionFactory
    {
      get
      {
        return MvcApplication.SessionFactory;
      }
    }

    public override void OnActionExecuting(
      ActionExecutingContext filterContext)
    {
      var session = sessionFactory.OpenSession();
      CurrentSessionContext.Bind(session);
    }

    public override void OnActionExecuted(
      ActionExecutedContext filterContext)
    {
      var session = CurrentSessionContext.Unbind(sessionFactory);
      session.Close();
    }

  }
}
