<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BlogMongoDB.Models.Post>" %>
<h2><%= Html.Encode(Model.Title) %></h2>
<br />
<%= Html.Encode(Model.Content) %>