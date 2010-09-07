<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<BlogMongoDB.Models.Tag>>" %>
<ul id="taglist">
<% foreach (var tag in Model) { %>
    <li><%=tag.Name %>,</li>
<% } %>
</ul>