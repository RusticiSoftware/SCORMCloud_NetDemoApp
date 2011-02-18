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
using System.Collections.Generic;
using System.Text;

namespace HostedDemoApp
{
    public partial class CloudCourseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<CourseData> courseList = ScormCloud.CourseService.GetCourseList();

            StringBuilder ct = new StringBuilder();

            ct.AppendLine("<table class=\"coursesTable\">");

            foreach (CourseData cd in courseList)
            {
                ct.AppendLine("<tr>");
                ct.AppendLine("<td>" + cd.Title + "</td>");
                ct.AppendLine("<td><a href='Delete.aspx?courseid=" + cd.CourseId + "'>Delete</a></td>");
                ct.AppendLine("<td><a href='Preview.aspx?redirect=CloudCourseList.aspx&courseid=" + cd.CourseId + "'>Preview</a></td>");
                ct.AppendLine("<td><a href='Properties.aspx?courseid=" + cd.CourseId + "'>Properties</a></td>");
                ct.AppendLine("</tr>");



            }


            ct.AppendLine("</table>");

            coursesTableDiv.InnerHtml = ct.ToString();
        }
    }
}
