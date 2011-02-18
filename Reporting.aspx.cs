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
    public partial class Reporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String reportageUrl = ScormCloud.ReportingService.LaunchReportageUrl();

            //Send user on their way
            Response.Redirect(reportageUrl);

        }
    }
}
