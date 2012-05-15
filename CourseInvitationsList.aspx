<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseInvitationsList.aspx.cs" Inherits="HostedDemoApp.CourseInvitationsList" MasterPageFile="~/LearningPages.Master" Title="Hosted Demo App" %>


<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div id="listwrapper" runat="server">
        <h2>Course Invitations</h2>
        
        <div ID="invitationsTableDiv" runat="server"></div>
        
        <br/><br/>
        <h2>Create new invitation</h2>
        <form action="CreateInvitationSample.aspx" method="Post" enctype="multipart/form-data">
        	
        <input id="hiddenCourseId" type="hidden" name="courseid" value="<%=courseId %>" />
        <br/>
        Sender email: <input type="text" name="creatingUserEmail"/>
        <br/>
        <input type="checkbox" name="send"/> send
        <br/>
        <input type="checkbox" name="public"/> public
        <br />
        To addresses: <input type="text" name="addresses"/> (comma-delimited)
        <br />
        <input type="submit" name="submit" value="Submit" />
        </form>
        
    </div>            
</asp:Content>
