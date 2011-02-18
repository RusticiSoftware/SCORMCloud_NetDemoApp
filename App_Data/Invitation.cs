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
    public class Invitation
    {
        private String _id;
        private String _learningId;
        private String _senderEmail;
        private String _senderName;

        public Invitation()
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

        public String SenderEmail
        {
            get { return _senderEmail; }
            set { _senderEmail = value; }
        }

        public String SenderName
        {
            get { return _senderName; }
            set { _senderName = value; }
        }
    }
}
