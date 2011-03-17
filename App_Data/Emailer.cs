/* Software License Agreement (BSD License)
 * 
 * Copyright (c) 2010-2011, Rustici Software, LLC
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the <organization> nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Rustici Software, LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

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
