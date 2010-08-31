<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<BlogSQL.Models.Tag>>" %>
<% string lst = ""; %>
<% foreach (var tag in Model) {
	   lst += tag.Name + ",";
 } %>
 <p>
    <label for="Tags">Tags:</label>
    <%= Html.TextBox("Tags", lst) %>
    <%= Html.ValidationMessage("Tags", "*") %>
</p>