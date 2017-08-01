/* Software License Agreement (BSD License)
 * 
 * Copyright (c) 2010-2011, Rustici Software, LLC
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the <organization> nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Rustici Software, LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

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
                ct.AppendLine("<td><a href='Details.aspx?courseid=" + cd.CourseId + "'>Details</a></td>");
                ct.AppendLine("<td><a href='CourseInvitationsList.aspx?courseid=" + cd.CourseId + "'>Invitations</a></td>");
                ct.AppendLine("</tr>");



            }


            ct.AppendLine("</table>");

            coursesTableDiv.InnerHtml = ct.ToString();
        }
    }
}
