using System;

namespace Eg.Burrows
{
  public partial class _Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ProductGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      var productId = (Guid) ProductGridView.SelectedDataKey.Value;
      var url = string.Format("~/ViewProduct.aspx?ProductId={0}",
                              productId.ToString());
      Response.Redirect(url);
    }


  }
}
