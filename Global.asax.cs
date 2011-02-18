using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using RusticiSoftware.HostedEngine.Client;
using System.Web.Configuration;

namespace HostedDemoApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScormCloud.Configuration = 
                new RusticiSoftware.HostedEngine.Client.Configuration(
                        WebConfigurationManager.AppSettings["HostedEngineWebServicesUrl"],
                        WebConfigurationManager.AppSettings["HostedEngineAppId"],
                        WebConfigurationManager.AppSettings["HostedEngineSecurityKey"]);
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}