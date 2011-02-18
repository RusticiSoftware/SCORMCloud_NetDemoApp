using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;

namespace HostedDemoApp
{
    public class Emailer
    {
        private SmtpClient _client;
        private MailAddress _senderAddress;
        private String _emailMessage;
        private String _invitationId;
        private String _learningTitle;
        private String _learningDescription;

        public Emailer(String emailSenderAddress, String emailSenderName, String invitationId, 
                        String learningTitle, String learningDescription, String emailMessage)
        {
            _invitationId = invitationId;
            _emailMessage = emailMessage;
            _learningDescription = learningDescription;
            _learningTitle = learningTitle;

            //Setup sender address and email client
            _senderAddress = new MailAddress(emailSenderAddress, emailSenderName);
            _client = new SmtpClient();

            //See if we should use SSL for emailing,
            //if fails, just default to false
            try { _client.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["SmtpUseSSL"]); }
            catch { _client.EnableSsl = false; }
        }

        public void SendAdminEmail()
        {
            String adminLink = Utils.GetSiteName() + "/LearningSent.aspx?invitation=" + _invitationId;

            //Send email to the owner of this invitation
            MailMessage adminEmail = new MailMessage();
            adminEmail.From = _senderAddress;
            adminEmail.To.Add(_senderAddress);
            adminEmail.Subject = "Your invitation to learn has been sent!";
            adminEmail.Body = CreateEmailBody(_emailMessage, _learningTitle, _learningDescription, adminLink);
            _client.Send(adminEmail);
        }

        public void SendLearnerEmail(String regId, String email)
        {
            String learnerLink = Utils.GetSiteName() + "/Learner.aspx?registration=" + regId;

            //Send email to learner
            MailMessage invitiationEmail = new MailMessage();
            invitiationEmail.From = _senderAddress;
            invitiationEmail.ReplyTo = _senderAddress;
            invitiationEmail.To.Add(email);
            invitiationEmail.Subject = _senderAddress.DisplayName + " has sent you an invitation to learn!";
            invitiationEmail.Body = CreateEmailBody(_emailMessage, _learningTitle, _learningDescription, learnerLink);
            _client.Send(invitiationEmail);
        }

        protected String CreateEmailBody(String message, String learningTitle, String learningDescription, String link)
        {
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Message: " + message);
            emailBody.AppendLine("Learning Title: " + learningTitle);
            emailBody.AppendLine("Learning Description: " + learningDescription);
            emailBody.AppendLine("Link: " + link);
            return emailBody.ToString();
        }
    }
}
