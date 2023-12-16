using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

namespace project_Ecommerce.Models
{
    public class reg_data
    {

        public int sr { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobno { get; set; }
        public string pass { get; set; }
        public string pass1 { get; set; }
        public string address { get; set; }

        public int pincode { get; set; }
        public string landmark { get; set; }
        public HttpPostedFileBase user_pic { get; set; }
        public string user_icon { get; set; }

    }
}


 