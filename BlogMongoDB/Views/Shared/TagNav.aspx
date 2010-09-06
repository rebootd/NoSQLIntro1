<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<BlogMongoDB.Models.Tag>>" %>
<ul id="tagnav">
<% foreach (var tag in Model) { %>
    <li><%=tag.Name %>,</li>
<% } %>
</ul>