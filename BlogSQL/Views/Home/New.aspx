<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BlogSQL.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %> 
        <% using (Html.BeginForm()) {%> 
            <fieldset>
                <legend>Fields</legend>
                <p>
                    <label for="Hash">Hash:</label>
                    <%= Html.TextBox("Hash" ) %>
                    <%= Html.ValidationMessage("Hash", "*") %>
                </p>

                <p>
                    <label for="Title">Title:</label>
                    <%= Html.TextBox("Title") %>
                    <%= Html.ValidationMessage("Title", "*") %>
                </p>

                <p>
                    <label for="Content">Content:</label>
                    <%= Html.TextBox("Content") %>
                    <%= Html.ValidationMessage("Content", "*") %>
                </p>

				<p>
				  <% Html.RenderPartial("TagEntry", new List<BlogMongoDB.Models.Tag>()); %>
				</p>

                <p>
                    <input type="submit" value="Create" />
                </p>
            </fieldset> 
        <% } %> 
        <div>
            <%=Html.ActionLink("Back to List", "Index") %>
        </div> 

</asp:Content>
