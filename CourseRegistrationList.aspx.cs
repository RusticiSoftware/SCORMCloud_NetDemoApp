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
    public partial class CourseRegistrationList : System.Web.UI.Page
    {
        protected string courseId;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Request["courseid"];

            List<RegistrationData> regList =  ScormCloud.RegistrationService.GetRegistrationListForCourse(courseId);

            StringBuilder ct = new StringBuilder();

            ct.AppendLine("<table class=\"registrationsTableDiv\">");

            ct.AppendLine("<tr>");
            ct.AppendLine("<td> Reg ID</td>");
            ct.AppendLine("<td> Course ID</td>");

            ct.AppendLine("<td>Completion</td>");
            ct.AppendLine("<td>Success</td>");
            ct.AppendLine("<td>Score</td>");
            ct.AppendLine("<td>TotalTime</td>");
            ct.AppendLine("<td>Completed Date </td>");
            ct.AppendLine("</tr>");

            foreach (RegistrationData reg in regList)
            {
                ct.AppendLine("<tr>");
                ct.AppendLine("<td>" + reg.RegistrationId + "</td>");
                ct.AppendLine("<td>" + reg.CourseId + "</td>");
                RegistrationSummary result = ScormCloud.RegistrationService.GetRegistrationSummary(reg.RegistrationId);
                ct.AppendLine("<td>" + result.Complete + "</td>");
                ct.AppendLine("<td>" + result.Success + "</td>");
                ct.AppendLine("<td>" + result.Score + "</td>");
                ct.AppendLine("<td>" + result.TotalTime + "</td>");
                String launchUrl = ScormCloud.RegistrationService.GetLaunchUrl(reg.RegistrationId,"http://cloud.scorm.com");
                ct.AppendLine("<td><a href='"+ launchUrl +"'>Launch</a></td>");
                //Delete
                //Launch

               // ct.AppendLine("<td><a href='InvitationInfoSample.aspx?invid=" + inv.Id + "&courseid=" + courseId + "'>details</a></td>");
               // ct.AppendLine("<td><a href='InvitationChange.aspx?invid=" + inv.Id + "&enable=" + (!inv.AllowLaunch).ToString().ToLower() + "&open=" + inv.AllowNewRegistrations.ToString().ToLower() + "&courseid=" + courseId + "'>" + (inv.AllowLaunch ? "enabled" : "disabled") + "</a></td>");
               // ct.AppendLine("<td><a href='InvitationChange.aspx?invid=" + inv.Id + "&enable=" + inv.AllowLaunch.ToString().ToLower() + "&open=" + (!inv.AllowNewRegistrations).ToString().ToLower() + "&courseid=" + courseId + "'>" + (inv.AllowNewRegistrations ? "open" : "closed") + "</a></td>");
                ct.AppendLine("</tr>");



            }


            ct.AppendLine("</table>");

            registrationsTableDiv.InnerHtml = ct.ToString();
        }

    }
}
