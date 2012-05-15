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
    public partial class InvitationChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String invid = Request["invid"];
            bool enable = bool.Parse(Request["enable"]);
            bool open = bool.Parse(Request["open"]);
            String courseId = Request["courseid"];

            ScormCloud.InvitationService.ChangeStatus(invid, enable, open);

            Response.Redirect(Utils.GetSiteName() + "CourseInvitationsList.aspx?courseid=" + courseId);


        }
    }
}
