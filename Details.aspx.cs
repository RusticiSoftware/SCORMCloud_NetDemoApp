using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RusticiSoftware.HostedEngine.Client;
using System.Text;

namespace HostedDemoApp
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Application specific authentication, or other logic, goes here

            //Grab course id from request
            String courseId = Request["courseid"];

            //Get properties editor url from ScormCloud client lib
            CourseDetail courseDetail = ScormCloud.CourseService.GetCourseDetail(courseId);

            StringBuilder ct = new StringBuilder();

            ct.AppendLine("<table class=\"coursesTable\">");
                ct.AppendLine("<tr>");
                ct.AppendLine("<td>Title</td><td>" + courseDetail.Title + "</td>");
                ct.AppendLine("</tr><tr>");
                ct.AppendLine("<td>CourseId</td><td>" + courseDetail.CourseId + "</td>");
                ct.AppendLine("</tr><tr>");
                ct.AppendLine("<td>Number of Versions</td><td>" + courseDetail.NumberOfVersions + "</td>");
                ct.AppendLine("</tr><tr>");
                ct.AppendLine("<td>Number of Registrations</td><td>" + courseDetail.NumberOfRegistrations + "</td>");
                ct.AppendLine("</tr><tr>");
                ct.AppendLine("<td>Learning Standard</td><td>" + courseDetail.LearningStandard + "</td>");
                ct.AppendLine("</tr><tr>");
                ct.AppendLine("<td>Tin Can / xAPI Activity ID</td><td>" + courseDetail.TincanActivityId + "</td>");
                ct.AppendLine("</tr>");
            ct.AppendLine("</table>");

            courseDetailDiv.InnerHtml = ct.ToString();
        }
    }
}