<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadLearning.aspx.cs" Inherits="HostedDemoApp.UploadLearning" MasterPageFile="~/LearningPages.Master" Title="Upload Learning" %>
<%@ Register TagPrefix="scormcloud" Namespace="RusticiSoftware.HostedEngine.Client.WebControls" Assembly="RusticiSoftware.HostedEngine.Client" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div id="questionBox" runat="server">
        <h2>What do you want to send?</h2>
        <scormcloud:ImportControl id="importForm" runat="server"></scormcloud:ImportControl>
    </div>            
</asp:Content>
