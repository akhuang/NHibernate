<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eg.Burrows._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="ProductGridView" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataKeyNames="Id" DataSourceID="ProductDataSource" ForeColor="#333333" 
            GridLines="None" onselectedindexchanged="ProductGridView_SelectedIndexChanged">
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    SortExpression="Description" />
                <asp:BoundField DataField="UnitPrice" DataFormatString="{0:c}" 
                    HeaderText="UnitPrice" SortExpression="UnitPrice" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                Sorry. No Products to display.
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
        </div>
    <asp:ObjectDataSource ID="ProductDataSource" runat="server" EnablePaging="True" 
        MaximumRowsParameterName="pageSize" SelectCountMethod="CountAll" 
        SelectMethod="FindAll" SortParameterName="sortExpression" 
        StartRowIndexParameterName="startRow" TypeName="Eg.Burrows.ProductDAO">
    </asp:ObjectDataSource>
    </form>
</body>
</html>
