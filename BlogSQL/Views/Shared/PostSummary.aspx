<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogSQL.Models.Post>" %>
<li>
    <a href="/<%= Model.Published.Year %>/<%= Model.Published.Month %>/<%= Model.Hash %>"><%= Model.Title %></a>
    <br />
    <%= Html.Encode(Model.Content) %>
    <br />
	<% Html.RenderPartial("TagList", Model.Tags); %>
    <br /><br />
    <% if (Session["author"] != null) { %>
    <%= Html.ActionLink("Edit", "Edit", "Home", new { id = Model.Id }, null)%>
    <% } %>
</li>
