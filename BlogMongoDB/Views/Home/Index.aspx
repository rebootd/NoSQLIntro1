<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<BlogMongoDB.Models.Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Recent Posts</h2>
    <ul id="postlist">
    <% foreach (var post in Model) { %>
    <li>
        <%= Html.ActionLink(post.Title, "Show", new { id = post.Id })%>
        <br />
        <%= Html.Encode(post.Content) %>
        <% if (Session["author"] != null) { %>
        <br />
        <%= Html.ActionLink("Edit", "Edit", new { id = post.Id })%>
        <% } %>
    </li>
    <% } %>
    </ul>
</asp:Content>
