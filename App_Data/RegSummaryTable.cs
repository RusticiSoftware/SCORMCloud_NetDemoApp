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
