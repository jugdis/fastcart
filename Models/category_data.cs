using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_Ecommerce.Models
{
    public class category_data
    {
        public string cat_name {  get; set; }
        public int cat_id { get; set; }
        public HttpPostedFileBase cat_icon { get; set; }

        
    }
}