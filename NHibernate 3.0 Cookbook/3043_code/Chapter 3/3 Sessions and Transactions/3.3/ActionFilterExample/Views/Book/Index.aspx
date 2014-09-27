<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Eg.Core.Book>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Books
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Books</h2>
    <% foreach (var book in Model)
       {%>
    <div>
        <h3>
            <%=Html.Encode(book.Name) %></h3>
        <p>
            ~ <%=Html.Encode(book.Author) %><br />
            <label>ISBN:</label><%=Html.Encode(book.ISBN) %></p>
        <p><%=Html.Encode(book.Description) %></p>
    </div>
    <% } %>
</asp:Content>
