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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net.Mail;
using RusticiSoftware.HostedEngine.Client;

namespace HostedDemoApp
{
    public partial class SendLearning : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            //If post back, create the invitation and forward
            //user to learning sent page
            if (IsPostBack) {
                String invitationId = CreateAndSendInvitation();
                Response.Redirect("LearningSent.aspx?invitation=" + invitationId);
            }
            else {
                String learningIdStr = Request["learningid"];

                //If a title is specified on query string, 
                //set that as initial value (and persist it to data store)
                String title = Request["title"];
                if (!String.IsNullOrEmpty(title)) {
                    learningTitle.Value = title;
                    DataStore.SetLearningTitle(learningIdStr, title);
                }

                //Set these learningid attributes for Ajax functionality
                learningTitle.Attributes["learningid"] = learningIdStr;
                learningDescription.Attributes["learningid"] = learningIdStr;

                //Setup preview learning and change settings links
                previewLearningLink.HRef = "Preview.aspx?courseid=" + learningIdStr;
                changeSettingsLink.HRef = "Properties.aspx?courseid=" + learningIdStr;

                //Hidden form value
                learningId.Value = learningIdStr;
            }
        }

        protected string CreateAndSendInvitation()
        {
            //Grab parameters, create new invitation id
            String invitationId = Guid.NewGuid().ToString();
            String learningIdVal = learningId.Value;
            String emailsVal = emails.Value;
            String emailSenderNameVal = emailSenderName.Value;
            String emailSenderAddressVal = emailSenderAddress.Value;
            String emailMessageVal = emailMessage.Value;
            String learningTitleVal = learningTitle.Value;
            String learningDescriptionVal = learningDescription.Value;
           
            

            //Persist this invitation to our local data store
            DataStore.AddInvitation(invitationId, learningIdVal, emailSenderAddressVal, emailSenderNameVal);

            Emailer emailer = new Emailer(emailSenderAddressVal, emailSenderNameVal, invitationId, 
                                          learningTitleVal, learningDescriptionVal, emailMessageVal);
            
            //Send admin email first
            emailer.SendAdminEmail();


            //Create registration and send email for everyone in email list
            String[] emailList = Utils.SplitAndTrim(emailsVal, ',');
            foreach (String email in emailList) {
                String regId = Guid.NewGuid().ToString();

                //Create registration in hosted, if it fails, skip this learner
                try { 
                    ScormCloud.RegistrationService.CreateRegistration(regId, learningIdVal, email, email, email); 
                }
                catch { continue; }

                DataStore.AddRegistration(learningIdVal, regId, email);
                emailer.SendLearnerEmail(regId, email);
            }


            return invitationId;
        }



        
    }
}
