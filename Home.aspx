<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HostedDemoApp.Home" MasterPageFile="~/LearningPages.Master" Title="Hosted Demo App" %>


<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div id="questionBox" runat="server">
        <div runat="server" id="pingDiv"></div>
        <div runat="server" id="authPingDiv"></div>
        <br />
        <h2>What do you want to do?</h2>
        
        <h3><a href="UploadLearning.aspx">Add some training</a></h3>
        <br />
        <h3><a href="CloudCourseList.aspx">Cloud Course List</a></h3>
        <br />
        <h3><a href="Reporting.aspx">View Reportage</a></h3>
        
    </div>            
</asp:Content>
