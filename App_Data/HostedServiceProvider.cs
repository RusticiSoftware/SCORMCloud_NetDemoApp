using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RusticiSoftware.HostedEngine.Client;
using System.Web.Configuration;

namespace HostedDemoApp
{
    public class HostedServiceProvider
    {
        private static ScormEngineService _service = null;

        private static String _serviceUrl = WebConfigurationManager.AppSettings["HostedEngineWebServicesUrl"];
        private static String _appId = WebConfigurationManager.AppSettings["HostedEngineAppId"];
        private static String _securityKey = WebConfigurationManager.AppSettings["HostedEngineSecurityKey"];

        public static ScormEngineService getInstance()
        {
            if(_service == null){
                _service = new ScormEngineService(_serviceUrl, _appId, _securityKey);
            }
            return _service;
        }
    }
}
