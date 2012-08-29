<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadLearning.aspx.cs" Inherits="HostedDemoApp.UploadLearning" MasterPageFile="~/LearningPages.Master" Title="Upload Learning" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div id="questionBox" runat="server">
        <h2>What do you want to send?</h2>
        
        <form ID="importCourseForm" method="post" runat="server" enctype="multipart/form-data">
              <input type="file" name="filedata" size="40" /> <br />
              <input type="submit" value="Send" />
        </form>

    </div>            
</asp:Content>
