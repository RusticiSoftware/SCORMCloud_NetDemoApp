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
using System.Net.Mail;
using System.Text;
using RusticiSoftware.HostedEngine.Client;
using System.Collections.Generic;
using System.Xml;

namespace HostedDemoApp
{
    public partial class LearningSent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Make sure invitation parameter is available
            String invitationId = Request["invitation"];
            if (invitationId == null) {
                Response.Write("No invitation id specified!");
                Response.End();
            }
            //Grab the learning id from the invitation info
            Invitation invitation = DataStore.GetInvitation(invitationId);
            String learningId = invitation.LearningId;

            //Set these learningid attributes for Ajax functionality
            learningTitle.Attributes["learningid"] = learningId;
            learningDescription.Attributes["learningid"] = learningId;

            //Set title and description of learning
            Learning learning = DataStore.GetLearning(learningId);
            learningTitle.Value = learning.Title;
            learningDescription.Value = learning.Description;

            //Setup preview learning and change settings links
            previewLearningLink.HRef = "Preview.aspx?courseid=" + learningId;
            changeSettingsLink.HRef = "Properties.aspx?courseid=" + learningId;



            //Construct registration table for all learners associated with this learning
            RegSummaryTable regTable = new RegSummaryTable(true);
            String[] regIds = DataStore.GetRegistrationIdsForLearning(learningId);
            foreach (String regId in regIds) {

                //Get completion, score, and other stats from Hosted Scorm Engine
                RegistrationSummary summary = ScormCloud.RegistrationService.GetRegistrationSummary(regId);
                
                //Add this info to our table
                regTable.AddRow(DataStore.GetRegistration(regId), 
                    summary.TotalTime, summary.Complete, summary.Success, summary.Score);

            }
            learnerTableDiv.InnerHtml = regTable.GetHtml();

        }

    }
}
