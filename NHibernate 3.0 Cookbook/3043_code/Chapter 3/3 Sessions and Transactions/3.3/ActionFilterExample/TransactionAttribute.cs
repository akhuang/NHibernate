using System;
using System.Web.Mvc;
using NHibernate;

namespace ActionFilterExample
{

  [AttributeUsage(AttributeTargets.Method,
    AllowMultiple=true)]
  public class TransactionAttribute 
    : NHibernateSessionAttribute 
  {

    protected ISession session
    {
      get
      {
        return sessionFactory.GetCurrentSession();
      }
    }

    public override void OnActionExecuting(
      ActionExecutingContext filterContext)
    {
      base.OnActionExecuting(filterContext);
      session.BeginTransaction();
    }

    public override void OnResultExecuted(
      ResultExecutedContext filterContext)
    {
      var tx = session.Transaction;
      if (tx != null && tx.IsActive)
        session.Transaction.Commit();

      base.OnResultExecuted(filterContext);
    }

  }
}
