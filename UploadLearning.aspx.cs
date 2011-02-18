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
using System.Text;
using System.Collections.Generic;

namespace HostedDemoApp
{
    public partial class UploadLearning : System.Web.UI.Page
    {
        protected String importUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            importForm.CourseId = Guid.NewGuid().ToString();
            importForm.Success += new EventHandler(importForm_Success);
        }

        void importForm_Success(object sender, EventArgs e)
        {
            String learningId = importForm.UploadedCourseId;
            String title = importForm.ImportResults[0].Title;
            DataStore.AddEmptyLearningRecord(learningId);
            Response.Redirect(ResolveUrl("~/SendLearning.aspx?learningid=" + learningId + "&title=" + title));
        }
    }
}
