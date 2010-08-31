<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<BlogRavenDB.Models.Post>>" %>
<ul id="postlist">
<% foreach (var post in Model) { %>
    <% Html.RenderPartial("PostSummary", post); %>
<% } %>
</ul>