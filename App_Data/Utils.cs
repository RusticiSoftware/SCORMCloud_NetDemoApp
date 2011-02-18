using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using RusticiSoftware.HostedEngine.Client;

namespace HostedDemoApp
{
    public class Utils
    {
        protected static String _siteName = ConfigurationManager.AppSettings["SiteName"];

        public static string GetSiteName()
        {
            return _siteName;
        }

        public static String[] SplitAndTrim(String list, Char splitVal)
        {
            String[] vals = list.Split(splitVal);
            for (int i = 0; i < vals.Length; i++) {
                vals[i] = vals[i].Trim();
            }
            return vals;
        }
    }
}
