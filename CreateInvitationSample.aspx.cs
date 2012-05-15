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
    public partial class CreateInvitationSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String courseId = Request["courseid"];
            String creatingUserEmail = Request["creatingUserEmail"];
            bool send = (Request["send"] != null && Request["send"].ToLower().Equals("on"));
            bool isPublic = (Request["public"] != null && Request["public"].ToLower().Equals("on"));
            String addresses = Request["addresses"];

            ScormCloud.InvitationService.CreateInvitation(courseId, isPublic, send, addresses,null,null,creatingUserEmail,false);

            Response.Redirect(Utils.GetSiteName() + "/CourseInvitationsList.aspx?courseid=" + courseId);

        }
    }
}
