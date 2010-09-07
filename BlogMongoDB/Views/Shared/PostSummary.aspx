<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogMongoDB.Models.Post>" %>
<li>
    <%= Html.ActionLink(Model.Title, "Show", "Home", new { id = Model.Id }, null)%>
    <br />
    <%= Html.Encode(Model.Content) %>
    <br />
	<% Html.RenderPartial("TagList", Model.Tags); %>
    <br /><br />
    <% if (Session["author"] != null) { %>
    <%= Html.ActionLink("Edit", "Edit", "Home", new { id = Model.Id }, null)%>
    <% } %>
</li>
