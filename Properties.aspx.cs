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
    public partial class Properties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Application specific authentication, or other logic, goes here

            //Grab course id from request
            String courseId = Request["courseid"];

            //Get properties editor url from ScormCloud client lib
            String propertyEditorUrl = ScormCloud.CourseService.GetPropertyEditorUrl(courseId);

            //Send user on their way
            Response.Redirect(propertyEditorUrl);
        }
    }
}
