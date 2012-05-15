<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvitationInfoSample.aspx.cs" Inherits="HostedDemoApp.InvitationInfoSample"  MasterPageFile="~/LearningPages.Master" Title="Hosted Demo App" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div id="listwrapper" runat="server">
        <h2>Invitation Info</h2>
        
        <div ID="invitationJobStatus" runat="server"></div>
        <br />
        <div ID="invitationsInfoDiv" runat="server"></div>
        
    </div>            
</asp:Content>
