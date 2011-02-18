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
    public class Learning
    {
        private String _id;
        private String _title;
        private String _description;

        public Learning()
        {
        }

        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
