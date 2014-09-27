using System;
using System.Web.UI;
using Eg.Core;
using NHibernate.Burrow.WebUtil.Attributes;

namespace Eg.Burrows
{
  public partial class EditProduct : UserControl
  {

    [EntityField]
    protected Product product;

    public event EventHandler Updated;
    public event EventHandler Cancelled;

    public void Bind(Product product)
    {
      this.product = product;
      if (product == null) return;
      txtProductName.Text = product.Name;
      txtDescription.Text = product.Description;
      txtUnitPrice.Text = product.UnitPrice.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      product.Name = txtProductName.Text;
      product.Description = txtDescription.Text;
      product.UnitPrice = decimal.Parse(txtUnitPrice.Text);
      if (Updated != null)
        Updated(this, new EventArgs());
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      product = null;
      if (Cancelled != null)
        Cancelled(this, new EventArgs());
    }



  }
}