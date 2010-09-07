<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<string>>" %>
<h3>Tags</h3>
<ul id="tagcloud">
<% foreach (var tag in Model) { %>
    <li><a href="/tags/show/<%= Html.Encode( tag ) %>"><%= Html.Encode( tag ) %></a></li>
<% } %>
</ul>