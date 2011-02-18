<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LearningSent.aspx.cs" Inherits="HostedDemoApp.LearningSent" MasterPageFile="~/LearningPages.Master" Title="Learning Sent" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready( function() {
            //Append thick box iframe params to appropriate links
            var link = $("#<%=changeSettingsLink.ClientID%>");
            link.attr('href', link.attr('href') + "&TB_iframe=true&keepThis=true&height=400&width=600");
            
            $(".detailedReportLink").each( function() {
                var link = $(this);
                link.attr('href', link.attr('href') + "&TB_iframe=true&keepThis=true&height=500&width=500");
            });
            
            
            //Hook up Ajax functionality to title and description fields
            $("#<%=learningTitle.ClientID%>").blur( function() {
                $.post('AjaxFunctions.aspx',
                       {'action':'saveLearningTitle',
                        'learningid':$(this).attr('learningid'),
                        'data':this.value});
            });
            
            $("#<%=learningDescription.ClientID%>").blur( function() {
                $.post('AjaxFunctions.aspx',
                       {'action':'saveLearningDescription',
                        'learningid':$(this).attr('learningid'),
                        'data':this.value }); 
            }); 
        });
     </script>
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div>
        <div id="learningDetailsBox">
            
            
            <div id="learningDetailsFields">
                <h2>You sent this...</h2>    
                <table id="learningDetailsTable">
                    <tr>
                        <td>Title</td>
                        <td><input id="learningTitle" runat="server" learningid="" type="text" /></td>
                    </tr>
                    <tr>
                        <td>Description</td>
                        <td><textarea id="learningDescription" runat="server" learningid="" rows="6"></textarea></td>
                    </tr>
                </table>
            </div>
            
            <div id="learningDetailsLinks">
                <div id="previewLearningButton" class="buttonDiv">
                    <a id="previewLearningLink" runat="server" target="_blank" class="buttonLink">Preview Learning</a>    
                </div>
                <div id="changeSettingsButton" class="buttonDiv">
                    <a id="changeSettingsLink" runat="server" class="thickbox buttonLink">Change Settings</a>
                </div>
            </div>

        </div>
        
        <div id="sentToDetailsBox">
            <h2>To...</h2>
            <div id="learnerTableDiv" runat="server">
            </div>
        </div>
    </div>
</asp:Content>
