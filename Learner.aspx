<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Learner.aspx.cs" Inherits="HostedDemoApp.Learner" MasterPageFile="~/LearningPages.Master" Title="Learner" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready( function() {
            $(".detailedReportLink").each( function() {
                var link = $(this);
                link.attr('href', link.attr('href') + "&TB_iframe=true&keepThis=true&height=500&width=500");
            });
        });
    </script>
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
   
    
            <h2 id="sentMessage" runat="server"></h2>
            <table id="learningDetailsTable">
                <tr>
                    <td valign="top">To</td>
                    <td><input id="learnerEmail" runat="server" disabled="disabled" size="60" /></td>
                </tr>
                <tr>
                    <td valign="top">From</td>
                    <td><input id="senderEmail" runat="server" disabled="disabled" size="60" /></td>
                </tr>
                <tr>
                    <td valign="top">Title</td>
                    <td><input id="learningTitle" runat="server" disabled="disabled" learningid="" type="text" size="60" /></td>
                </tr>
                <tr>
                    <td valign="top">Description</td>
                    <td><textarea id="learningDescription" runat="server" disabled="disabled" learningid="" cols="60" rows="6"></textarea></td>
                </tr>
                <tr>
                    <td valign="top">So far...</td>
                    <td>
                        <div id="learnerInfoTableDiv" runat="server">
                        </div>
                    </td>
                </tr>
            </table>

    
            <div id="startLearningDiv" class="buttonDiv">
                <a id="startLearningLink" class="buttonLink" runat="server" >Start Learning (or continue)</a>
            </div>
            <div id="detailedReportDiv" class="buttonDiv">
                <a id="detailedReportLink" class="thickbox detailedReportLink buttonLink" runat="server" href="#">View Detailed Progress</a>
            </div>
    

</asp:Content>
