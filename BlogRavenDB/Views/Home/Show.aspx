﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BlogRavenDB.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Show
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("PostDetail", Model); %>

</asp:Content>
