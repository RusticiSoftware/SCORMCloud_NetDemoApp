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

namespace HostedDemoApp
{
    public partial class Redirector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String url = GetRedirectUrl();
            if (url != null) {
                Response.Redirect(url);
            }
            else {
                writeErrorText("error: Page requested not found");
            }
        }

        protected String GetRedirectUrl()
        {
            String page = Request["page"];
            if (String.IsNullOrEmpty(page)){
                return null;
            }

            if (page.Equals("previewLearning")) {
                String learningId = Request["learningid"];
                String redirectOnExit = Request["redirectOnExit"];
                return ScormCloud.CourseService.GetPreviewUrl(learningId, redirectOnExit);
            }

            if (page.Equals("packagePropertyEditor")) {
                String learningId = Request["learningid"];
                return ScormCloud.CourseService.GetPropertyEditorUrl(learningId);
            }

            else if (page.Equals("launch")) {
                String regId = Request["regid"];
                String redirectOnExit = Request["redirectOnExit"];
                //Update last activity time, as user is being forwarded to launch page now
                DataStore.updateLastActivityTime(regId);
                return ScormCloud.RegistrationService.GetLaunchUrl(regId, redirectOnExit);
            }
            return null;
        }

        //Write the given errorText to it's appropriate place
        //TODO: Move this into a super class
        protected void writeErrorText(String errorText)
        {
            Response.Write(errorText);
            Response.End();
        }
    }
}
