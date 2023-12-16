using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_Ecommerce.Models
{
    public class subcategory_data
    {

        public string cat_name  { get; set; }
        public int cat_id { get; set; }
        public int subcat_id { get; set; }
        public string subcat_name { get; set; }
        public HttpPostedFileBase subcat_icon { get; set; }
        public string subcat_pic { get; set; }
        public int subcat_status { get; set; }

    }
}

