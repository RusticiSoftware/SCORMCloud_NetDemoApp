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
    public partial class Launch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Application specific authentication, or other logic, goes here

            //Grab reg id from request
            String regId = Request["regid"];

            //Use referrer page as default redirect
            String redirectOnExitUrl = Request.UrlReferrer.ToString();

            //Get launch url from ScormCloud client library
            String launchUrl = ScormCloud.RegistrationService.GetLaunchUrl(regId, redirectOnExitUrl);

            //Send user on their way
            Response.Redirect(launchUrl);
        }
    }
}
