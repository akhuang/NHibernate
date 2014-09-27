using System;

namespace Eg.Burrows
{
public partial class ViewProduct : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!IsPostBack)
    {
      Guid Id = new Guid(Request
        .QueryString["ProductId"]);
      editProduct.Bind(new ProductDAO().Get(Id));
    }
  }

  protected void editProduct_Updated(
    object sender, EventArgs e)
  {
    Response.Redirect("~/");
  }

  protected void editProduct_Cancelled(
    object sender, EventArgs e)
  {
    Response.Redirect("~/");
  }
}
}
