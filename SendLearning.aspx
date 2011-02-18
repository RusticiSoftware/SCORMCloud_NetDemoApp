<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendLearning.aspx.cs" Inherits="HostedDemoApp.SendLearning" MasterPageFile="~/LearningPages.Master" Title="Send Learning"%>

<asp:Content ID="headContent" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready( function() {
            //Append thick box iframe params to appropriate links
            var link = $("#<%=changeSettingsLink.ClientID%>");
            link.attr('href', link.attr('href') + "&TB_iframe=true&keepThis=true&height=400&width=600");
            
            
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
            
            $("#<%=sendLearningLink.ClientID%>").click( function() {
                if(validateForm()){
                    $("#<%=sendingForm.ClientID%>").submit();
                }
            });
            
            $("#<%=emails.ClientID%>").focus( function() {
                if($(this).val() == "enter a list of email addresses, separated by commas"){
                    $(this).val('');
                }
            });  
        });
        
        function validateForm()
        {
            var emails = $("#<%=emails.ClientID%>").val();
            var emailSenderName = $("#<%=emailSenderName.ClientID%>").val();
            var emailSenderAddress = $("#<%=emailSenderAddress.ClientID%>").val();
            var emailMessage = $("#<%=emailMessage.ClientID%>").val();
            
            if(!emails){
                displayFormError("Invitation must have a list of recipients");
                return false;
            }
            
            if(!emailSenderName){
                displayFormError("Email sender name cannot be blank");
                return false;
            }
            
            if(!emailSenderAddress){
                displayFormError("Email sender address cannot be blank");
                return false;
            }
            
            if(!emailMessage){
                displayFormError("Email message cannot be blank");
                return false;
            }
            
            emails = emails.split(",");
            var email = null;
            for (i in emails) {
                email = $.trim(emails[i]);
                if(!isValidEmail(email)){
                    displayFormError("The email address \"" + email + "\" entered in the list of recipients appears to be an invalid address");
                    return false;
                }
            }
            
            if(!isValidEmail($.trim(emailSenderAddress))){
                displayFormError("The email address \"" + emailSenderAddress + "\" does not appear to be a valid address.");
                return false;
            }
            
            return true;
        }
        
        function isValidEmail(email)
        {
            var pattern = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return pattern.test(email);
        }
        
        function displayFormError(message)
        {
            alert(message);
        }
    </script>
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <form id="sendingForm" runat="server" method="post">
    
        <div id="learningDetailsBox">
            <div id="learningDetailsFields">
                <h2>Send this...</h2>
                <table id="learningDetailsTable">
                    <tr>
                        <td>Title</td>
                        <td><input id="learningTitle" learningid="" runat="server" type="text" size="60" /></td>
                    </tr>
                    <tr>
                        <td>Description</td>
                        <td><textarea id="learningDescription" learningid="" runat="server" cols="60" rows="6"></textarea></td>
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
        
        <div id="sendToBox">
                <h2>To whom?</h2>
                <textarea id="emails" runat="server" cols="60" rows="6">enter a list of email addresses, separated by commas</textarea>
                <br />
                <h2>From?</h2>
                <table>
                    <tr>
                        <td>Your Name</td>
                        <td><input id="emailSenderName" runat="server" type="text" /></td>
                    </tr>
                    <tr>
                        <td>Email Address</td>
                        <td><input id="emailSenderAddress" runat="server" type="text" /></td>
                    </tr>
                    <tr>
                        <td>Message</td>
                        <td><textarea id="emailMessage" runat="server" cols="60" rows="6"></textarea></td>
                    </tr>
                </table>
                <div id="sendLearningButton" class="buttonDiv">
                    <a id="sendLearningLink" runat="server" class="buttonLink">Send Learning Now</a>
                </div>            
        </div>
    
        <input id="learningId" runat="server" type="hidden" />
        <input id="formSubmission" runat="server" type="hidden" value="true" />
    </form>
</asp:Content>
