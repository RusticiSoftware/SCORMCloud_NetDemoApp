using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RusticiSoftware.HostedEngine.Client;

namespace HostedDemoApp
{
    public partial class HostedReport : System.Web.UI.Page
    {
        protected String dataUrl = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            String regId = Request["registration"];

            ServiceRequest sr = ScormCloud.CreateNewRequest();
            sr.Parameters.Add("regid", regId);
            sr.Parameters.Add("resultsformat", "full");
            sr.Parameters.Add("format", "json");
            sr.Parameters.Add("jsoncallback", "getRegistrationResultCallback");
            dataUrl = sr.ConstructUrl("rustici.registration.getRegistrationResult");
        }
    }
}
