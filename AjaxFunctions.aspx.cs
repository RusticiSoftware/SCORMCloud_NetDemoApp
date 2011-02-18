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
    public partial class AjaxFunctions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Clear();
            Response.Write(GetResponse(sender, e));
            Response.Flush();
            Response.End();
        }

        protected String GetResponse(object sender, EventArgs e)
        {
            String action = Request["action"];

            if (String.IsNullOrEmpty(action)) {
                return "error: No action specified";
            }

            if (action.Equals("saveLearningTitle")) {
                String learningId = Request["learningid"];
                String title = Request["data"];
                DataStore.SetLearningTitle(learningId, title);
                return "completed";
            }

            if (action.Equals("saveLearningDescription")) {
                String learningId = Request["learningid"];
                String description = Request["data"];
                DataStore.SetLearningDescription(learningId, description);
                return "completed";
            }

            return "error: Action not found";
        }
    }
}
