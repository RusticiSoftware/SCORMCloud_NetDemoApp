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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ScormCloud.DebugService.CloudPing())
            {
                pingDiv.InnerHtml = "<p style='color:green;'>Cloud Ping Successful</p>";
            }
            else
            {
                pingDiv.InnerHtml = "<p style='color:red;'>Cloud Ping Not Successful</p>";
            }

            if (ScormCloud.DebugService.CloudAuthPing())
            {
                authPingDiv.InnerHtml = "<p style='color:green;'>Cloud Authentication Successful</p>";
            }
            else
            {
                authPingDiv.InnerHtml = "<p style='color:red;'>Cloud Authentication Not Successful</p>";
            }


        }
    }
}
