<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Session["author"] != null)
    {
%>
    <ul id="menu">              
        <li><%: Html.ActionLink("New Post", "New", "Home")%></li>
    </ul>
<%
    }
%>
