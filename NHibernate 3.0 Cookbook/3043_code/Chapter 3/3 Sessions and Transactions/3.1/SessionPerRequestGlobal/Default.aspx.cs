using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionPerRequestGlobal
{
  public partial class _Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      if (!Request.QueryString.AllKeys.Contains("id"))
      {
        var product = new Eg.Core.Book()
        {
          Name = "NHibernate 3.0 Cookbook",
          Description = "The greatest book ever",
          UnitPrice = 50M,
          ISBN = "3042",
          Author = "Jason Dentler"
        };

        var session = Global.SessionFactory.GetCurrentSession();
        using (var tran = session.BeginTransaction())
        {
          session.SaveOrUpdate(product);
          tran.Commit();
        }
        Response.Redirect("?id=" + product.Id.ToString());
      }
      else
      {
        Guid productId = new Guid(Request["id"]);
        Eg.Core.Product product;
        var session = Global.SessionFactory.GetCurrentSession();
        using (var tran = session.BeginTransaction())
        {
          product = session.Get<Eg.Core.Product>(productId);
          tran.Commit();
        }
        Page.Title = product.Name;
        Label1.Text = product.Name;
        Label2.Text = product.Description;
      }
    }
  }
}
