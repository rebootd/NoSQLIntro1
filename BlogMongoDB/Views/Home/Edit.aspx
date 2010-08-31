<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BlogMongoDB.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Update was unsuccessful. Please correct the errors and try again.") %> 
        <% using (Html.BeginForm()) {%> 
            <fieldset>
                <legend>Fields</legend>
                <p>
                    <label for="Hash">Hash:</label>
                    <%= Html.TextBox("Hash", Model.Hash ) %>
                    <%= Html.ValidationMessage("Hash", "*") %>
                </p>

                <p>
                    <label for="Title">Title:</label>
                    <%= Html.TextBox("Title", Model.Title) %>
                    <%= Html.ValidationMessage("Title", "*") %>
                </p>

                <p>
                    <label for="Content">Content:</label>
                    <%= Html.TextBox("Content", Model.Content) %>
                    <%= Html.ValidationMessage("Content", "*") %>
                </p>

				<p>
				  <% Html.RenderPartial("TagEntry", Model.Tags); %>
				</p>

                <p>
                    <input type="submit" value="Update" />
                </p>
            </fieldset> 
        <% } %> 
        <div>
            <%=Html.ActionLink("Back to List", "Index") %>
        </div> 

</asp:Content>
