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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using RusticiSoftware.HostedEngine.Client;

namespace HostedDemoApp
{
    public class RegSummaryTable
    {
        private bool _isAdmin = false;
        private StringBuilder _rowsHtml;

        public RegSummaryTable() : this(false)
        {
        }

        public RegSummaryTable(bool isAdmin)
        {
            _isAdmin = isAdmin;
            _rowsHtml = new StringBuilder();
        }

        public void AddRow(Registration reg, String totalTime, String complete, String success, String score)
        {
            _rowsHtml.AppendLine(GetRowHtml(reg, totalTime, complete, success, score));
        }

        public String GetHtml()
        {
            return "<table class=\"regSummaryTable\">" + 
                    GetHeaderHtml() + 
                    _rowsHtml.ToString() + 
                    "</table>";
        }

        protected String GetHeaderHtml()
        {
            StringBuilder headerRow = new StringBuilder();

            headerRow.AppendLine("<tr>");
            if (_isAdmin) {
                headerRow.AppendLine("\t<th>Email</th>");
            }
            headerRow.AppendLine("\t<th>Last Activity</th>");
            headerRow.AppendLine("\t<th>Time Spent</th>");
            headerRow.AppendLine("\t<th>Finished?</th>");
            headerRow.AppendLine("\t<th>Passed?</th>");
            headerRow.AppendLine("\t<th>Score?</th>");
            headerRow.AppendLine("</tr>");

            return headerRow.ToString();
        }

        protected String GetRowHtml(Registration reg, String totalTime, String complete, String success, String score)
        {
            StringBuilder row = new StringBuilder();
            row.AppendLine("<tr>");

            if (_isAdmin) {
                row.AppendLine("\t<td>" + GetDetailedReportLink(reg.Id, reg.Email) + "</td>");
            }
            row.AppendLine("\t<td>" + reg.LastActivityTime.ToString() + "</td>");
            row.AppendLine("\t<td>" + totalTime + "</td>");
            row.AppendLine("\t<td>" + complete + "</td>");
            row.AppendLine("\t<td>" + success + "</td>");
            row.AppendLine("\t<td>" + score + "</td>");
            row.AppendLine("</tr>");

            return row.ToString();
        }

        protected String GetDetailedReportLink(String regId, String linkContents)
        {
            return "<a class=\"thickbox detailedReportLink\" " +
                    "href=\"HostedReport.aspx?registration=" + regId +
                    "\">" + linkContents + "</a>";
        }
    }
}
