<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogSQL.Models.Post>" %>
<li>
    <%= Html.ActionLink(Model.Title, "Show", new { id = Model.Id })%>
    <br />
    <%= Html.Encode(Model.Content) %>
    <% if (Session["author"] != null) { %>
    <br />
    <%= Html.ActionLink("Edit", "Edit", new { id = Model.Id })%>
    <% } %>
</li>
