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
