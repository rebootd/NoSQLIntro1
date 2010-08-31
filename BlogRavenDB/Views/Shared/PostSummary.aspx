<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogRavenDB.Models.Post>" %>
<li>
    <%= Html.ActionLink(Model.Title, "Show", new { id = Model.Id })%>
    <br />
    <%= Html.Encode(Model.Content) %>
    <% if (Session["author"] != null) { %>
    <br />
	<% Html.RenderPartial("TagList", Model.Tags); %>
    <br /><br />
    <%= Html.ActionLink("Edit", "Edit", new { id = Model.Id })%>
    <% } %>
</li>
