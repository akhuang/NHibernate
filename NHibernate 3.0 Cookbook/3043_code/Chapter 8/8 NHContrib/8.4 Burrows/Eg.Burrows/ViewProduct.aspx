<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProduct.aspx.cs" Inherits="Eg.Burrows.ViewProduct" %>
<%@ Register Src="EditProduct.ascx" 
TagName="EditProduct" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<uc1:EditProduct ID="editProduct" runat="server" 
OnUpdated="editProduct_Updated"
OnCancelled="editProduct_Cancelled">
</uc1:EditProduct>    
    </div>
    </form>
</body>
</html>
