using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace project_Ecommerce.Models
{
    public class product_data
    {

        public int product_id { get; set; }
        public string product_name {  get; set; }
        public int cat_id { get; set; }
        public int subcat_id { get; set; }

        [AllowHtml]
        public string dec { get; set; }
        public int mrp { get; set; }
        public int salerate { get; set; }
        public string brand { get; set; }
        public HttpPostedFileBase product_icon { get; set; }
        public string product_pic { get; set; }

        public string cat_name { get; set;}
        public string subcat_name { get; set;}
        public bool status { get; set;}
    }
}

