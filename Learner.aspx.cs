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
using System.Collections.Generic;
using System.Text;
using RusticiSoftware.HostedEngine.Client;
using System.Xml;

namespace HostedDemoApp
{
    public partial class Learner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String regId = Request["registration"];

            //Get registration info, make sure it exists
            Registration reg = DataStore.GetRegistration(regId);
            if (reg == null) {
                Response.Write("Error, registration not found");
                Response.End();
            }

            //Grab learning and invitation information as well
            String learningId = reg.LearningId;
            Learning learning = DataStore.GetLearning(learningId);
            Invitation invitation = DataStore.GetInvitationByLearningId(learningId);

            //Set dynamic content for controls
            sentMessage.InnerText = invitation.SenderName + " sent you some learning";
            learnerEmail.Value = reg.Email;
            senderEmail.Value = invitation.SenderEmail;
            learningTitle.Value = learning.Title;
            learningDescription.Value = learning.Description;



            //Contruct a single row table with this learner's info
            RegSummaryTable regTable = new RegSummaryTable(false);
            
            //Get completion, score, and other stats from Hosted Scorm Engine
            RegistrationSummary summary = ScormCloud.RegistrationService.GetRegistrationSummary(regId);

            //Add this info to our table
            regTable.AddRow(DataStore.GetRegistration(regId), summary.TotalTime, summary.Complete, summary.Success, summary.Score);

            learnerInfoTableDiv.InnerHtml = regTable.GetHtml();



            //Get launch url from ScormCloud client, redirect to this page on exit
            startLearningLink.HRef = "Launch.aspx?regid=" + regId;

            //Include detailed report link for this learner
            detailedReportLink.HRef = "HostedReport.aspx?registration=" + regId;
        }
    }
}
