<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProduct.ascx.cs"
    Inherits="Eg.Burrows.EditProduct" %>
<fieldset>
    <legend>Edit Product</legend>
    <table border="0">
        <tr>
            <td>
                <asp:Label 
                ID="lblProductName" runat="server" 
                Text="Name:" 
                AssociatedControlID="txtProductName">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtProductName" 
                runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDescription" runat="server" 
                Text="Description:" 
                    AssociatedControlID="txtDescription">
                    </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDescription" 
                runat="server" TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" 
                Text="Unit Price:" 
                    AssociatedControlID="txtUnitPrice">
                    </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtUnitPrice" 
                runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSave" 
    runat="server" Text="Save" 
    onclick="btnSave_Click" />
    <asp:Button ID="btnCancel"
        runat="server" Text="Cancel" 
        onclick="btnCancel_Click" />
</fieldset>
