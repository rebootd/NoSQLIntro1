<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogSQL.Models.Post>" %>
<li>
    <%= Html.ActionLink(Model.Title, "Show", "Home", new { id = Model.Id }, null)%>
    <br />
    <%= Html.Encode(Model.Content) %>
    <br />
	<% Html.RenderPartial("TagList", Model.Tags); %>
    <% if (Session["author"] != null) { %>
	<br /><br />
    <%= Html.ActionLink("Edit", "Edit", "Home", new { id = Model.Id }, null)%>
    <% } %>
</li>
