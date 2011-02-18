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
using RusticiSoftware.HostedEngine.Client;

namespace HostedDemoApp
{
    public partial class Redirector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = GetRedirectUrl();
            if (url != null) {
                Response.Redirect(url);
            }
            else {
                writeErrorText("error: Page requested not found");
            }
        }

        protected String GetRedirectUrl()
        {
            String page = Request["page"];
            if (String.IsNullOrEmpty(page)){
                return null;
            }

            if (page.Equals("previewLearning")) {
                String learningId = Request["learningid"];
                String redirectOnExit = Request["redirectOnExit"];
                return ScormCloud.CourseService.GetPreviewUrl(learningId, redirectOnExit);
            }

            if (page.Equals("packagePropertyEditor")) {
                String learningId = Request["learningid"];
                return ScormCloud.CourseService.GetPropertyEditorUrl(learningId);
            }

            else if (page.Equals("launch")) {
                String regId = Request["regid"];
                String redirectOnExit = Request["redirectOnExit"];
                //Update last activity time, as user is being forwarded to launch page now
                DataStore.updateLastActivityTime(regId);
                return ScormCloud.RegistrationService.GetLaunchUrl(regId, redirectOnExit);
            }
            return null;
        }

        //Write the given errorText to it's appropriate place
        //TODO: Move this into a super class
        protected void writeErrorText(String errorText)
        {
            Response.Write(errorText);
            Response.End();
        }
    }
}
