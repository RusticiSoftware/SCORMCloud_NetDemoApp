using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace HostedDemoApp
{
    public class Registration
    {
        private String _id;
        private String _learningId;
        private String _email;
        private DateTime _lastActivityTime;

        public Registration()
        {
        }

        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String LearningId
        {
            get { return _learningId; }
            set { _learningId = value; }
        }

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime LastActivityTime
        {
            get { return _lastActivityTime; }
            set { _lastActivityTime = value; }
        }
    }
}
