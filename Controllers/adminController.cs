using project_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace project_Ecommerce.Controllers
{
    [Authorize]
    public class adminController : Controller
    {
        // GET: admin
        dblayer db=new dblayer();
        datalayer dl=new datalayer();
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [AllowAnonymous,HttpPost]
        public ActionResult AdminLogin(string mobno, string pass) {
            if (mobno == "12345" && pass == "admin")
            {
                FormsAuthentication.SetAuthCookie("12345", true);
                return RedirectToAction("category");
            }
            else
            {
                return View();
            }
        }
        public ActionResult category()
        {
            return View();
        }


        [HttpPost]
        public ActionResult add_category(category_data c)
        {
            int r;
            if(c.cat_id > 0)
            {
                if(c.cat_icon != null)
                {
                    SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("action",6),
                new SqlParameter("cat_id",c.cat_id),
                new SqlParameter("cat_name",c.cat_name),
                new SqlParameter("cat_icon",c.cat_icon.FileName)
                };

                    r = db.ExcuteDML("sp_category", sp);
                    if (r>0 )
                    {
                        c.cat_icon.SaveAs(Server.MapPath("/content/categoryicon/") + c.cat_icon.FileName);
                    }

                }
                else
                {
                    SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("action",7),
                new SqlParameter("cat_id",c.cat_id),
                new SqlParameter("cat_name",c.cat_name)
                
                };

                    r = db.ExcuteDML("sp_category", sp);
                }
               
            }

            else
            {
                SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("action",1),
                new SqlParameter("cat_name",c.cat_name),
                new SqlParameter("cat_icon",c.cat_icon.FileName)
            };
               object  k = db.ExcuteScalar("sp_category", sp);
                if (k != null)
                {
                    c.cat_icon.SaveAs(Server.MapPath("/content/categoryicon/") + c.cat_icon.FileName);
                }
                return Json(k, JsonRequestBehavior.AllowGet);

            }
            
            return Json(r,JsonRequestBehavior.AllowGet);
        }


        public ActionResult print_table_category()
        {
            DataTable dt = dl.SelectAllcategories();
            List<table_category> list= new List<table_category>();
            foreach(DataRow i in dt.Rows)
            {
                list.Add(new table_category() {
                 cat_name = i["cat_name"].ToString(),
                    cat_pic = i["cat_icon"].ToString(),
                    cat_status = Convert.ToInt32(i["cat_status"]),
                    cat_id = Convert.ToInt32(i["cat_id"])
                });

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //deleting record from category table
        [HttpPost]
        public ActionResult del_category(int cat_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("action",4),
            new SqlParameter("cat_id",cat_id)
            };
            db.ExcuteDML("sp_category", sp);
            return Json("Record Deleted", JsonRequestBehavior.AllowGet);
        }

        //geting categoory
        [HttpPost]
        public ActionResult get_category(int cat_id)
        {
            SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter ("action",5),
            new SqlParameter("cat_id",cat_id)
            };
            DataTable dt = db.ExcuteGetTable("sp_category", sp);

            category_data list=new category_data()
            {
                cat_id = int.Parse(dt.Rows[0]["cat_id"].ToString()),
                cat_name = dt.Rows[0]["cat_name"].ToString(),
            };


            return Json(list,JsonRequestBehavior.AllowGet);

        }




        //bind category on subcategory view on page loade..
        public ActionResult sub_category()
        {
            DataTable dt = dl.SelectAllcategories();
            ViewBag.data = dt;
            return View();
        }


        [HttpPost]
        public ActionResult add_subcat(subcategory_data s)
        {
            int k;
           if (s.subcat_id > 0)
            {
                if(s.subcat_icon != null)
                {
                    SqlParameter[] sp = new SqlParameter[]
                    {
                        new SqlParameter("action",7),
                        new SqlParameter("subcat_name",s.subcat_name),
                        new SqlParameter("subcat_icon",s.subcat_icon.FileName),
                        new SqlParameter("subcat_id",s.subcat_id),
                        new SqlParameter("cat_id",s.cat_name)


                     };
                    k = db.ExcuteDML("sp_subcategory", sp);
                    if (k > 0)
                    {
                        s.subcat_icon.SaveAs(Server.MapPath("/content/categoryicon/") + s.subcat_icon.FileName);
                    }

                }
                else
                {

                    SqlParameter[] sp = new SqlParameter[]
                    {
                        new SqlParameter("action",8),
                        new SqlParameter("subcat_name",s.subcat_name),
                        new SqlParameter("subcat_id",s.subcat_id),
                        new SqlParameter("cat_id",s.cat_name)


                     };
                    k = db.ExcuteDML("sp_subcategory", sp);

                }

            }
            else
            {
                SqlParameter[] sp = new SqlParameter[]
                     {
                        new SqlParameter("action",1),
                        new SqlParameter("subcat_name",s.subcat_name),
                        new SqlParameter("subcat_icon",s.subcat_icon.FileName),
                        new SqlParameter("cat_id",s.cat_name)


                      };
                object r = db.ExcuteScalar("sp_subcategory", sp);
                if (r !=null )
                {
                    s.subcat_icon.SaveAs(Server.MapPath("/content/categoryicon/") + s.subcat_icon.FileName);
                }
                return Json(r, JsonRequestBehavior.AllowGet);
            }

            return Json(k, JsonRequestBehavior.AllowGet);
        }

        public ActionResult print_table_subcategory()
        {

            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter ("action",4)
            };
            DataTable dt = db.ExcuteGetTable("sp_subcategory", sp);
            List<subcategory_data> list = new List<subcategory_data>();
            foreach (DataRow i in dt.Rows)
            {
                list.Add(new subcategory_data()
                {
                    cat_name= i["cat_name"].ToString(),
                    subcat_id = Convert.ToInt32(i["subcat_id"]),
                    cat_id = Convert.ToInt32(i["cat_id"]),
                    subcat_name = i["subcat_name"].ToString(),
                    subcat_pic = i["subcat_icon"].ToString(),
                    subcat_status = Convert.ToInt32(i["subcat_status"])
                });

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //deleting record from subcategory table
        [HttpPost]
        public ActionResult del_subcategory(int subcat_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("action",5),
            new SqlParameter("subcat_id",subcat_id)
            };
            db.ExcuteDML("sp_subcategory", sp);
            return Json("Record Deleted", JsonRequestBehavior.AllowGet);
        }

        // geting data or subcategory
        [HttpPost]
        public ActionResult get_subcategory(int subcat_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter ("action",6),
            new SqlParameter("subcat_id",subcat_id)
            };
            DataTable dt = db.ExcuteGetTable("sp_subcategory", sp);

            subcategory_data list = new subcategory_data()
            {
                subcat_id = int.Parse(dt.Rows[0]["subcat_id"].ToString()),
                subcat_name = dt.Rows[0]["subcat_name"].ToString(),
            };


            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult product()
        {
            DataTable dt= dl.SelectAllcategories();
            ViewBag.data=dt;

            return View();

        }

        //adding product in DataBase

        [HttpPost]
        public ActionResult add_product(product_data p)
        {
            SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter("action",1),
            new SqlParameter("product_name",p.product_name),
            new SqlParameter("cat_id",p.cat_id),
            new SqlParameter("subcat_id",p.subcat_id),
            new SqlParameter("dec",p.dec),
            new SqlParameter("mrp",p.mrp),
            new SqlParameter("salerate",p.salerate),
            new SqlParameter("brand",p.brand),
            new SqlParameter("product_icon",p.product_icon.FileName)
            };
            object r = db.ExcuteScalar("sp_product", sp);
            if(r !=null)
            {
                p.product_icon.SaveAs(Server.MapPath("/content/categoryicon/") + p.product_icon.FileName);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        //print table product 
        public ActionResult print_table_product()
        {
            SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter("action",3)
            };
            DataTable dt = db.ExcuteGetTable("sp_product", sp);
            List<product_data> list = new List<product_data>();
            foreach(DataRow i in dt.Rows)
            {
                list.Add(new product_data() {
                   product_id = Convert.ToInt32(i["product_id"].ToString()),
                    product_name = i["product_name"].ToString(),
                    cat_id= Convert.ToInt32(i["cat_id"]),
                    subcat_id= Convert.ToInt32(i["subcat_id"]),
                   cat_name = i["cat_name"].ToString(),
                   subcat_name = i["subcat_name"].ToString(),
                    dec = i["dec"].ToString(),
                    mrp = Convert.ToInt32(i["mrp"].ToString()),
                    salerate = Convert.ToInt32(i["salerate"].ToString()),
                    product_pic = i["product_icon"].ToString(),
                    brand = i["brand"].ToString()
                 //   status = Convert.ToBoolean(i["status"])

                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        //deleting record from product table
        [HttpPost]
        public ActionResult del_product(int product_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("action",4),
            new SqlParameter("product_id",product_id)
            };
            int r=db.ExcuteDML("sp_product", sp);
            return Json("Record Deleted", JsonRequestBehavior.AllowGet);
        }


        // geting data or product
        [HttpPost]
        public ActionResult get_product(int product_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter ("action",5),
            new SqlParameter("product_id",product_id)
            };
            DataTable dt = db.ExcuteGetTable("sp_product", sp);

            product_data list = new product_data()
            {
                product_id = Convert.ToInt32(dt.Rows[0]["product_id"].ToString()),
                product_name = dt.Rows[0]["product_name"].ToString(),
                cat_id = Convert.ToInt32(dt.Rows[0]["cat_id"]),
                subcat_id = Convert.ToInt32(dt.Rows[0]["subcat_id"]),
                cat_name = dt.Rows[0]["cat_name"].ToString(),
                subcat_name = dt.Rows[0]["subcat_name"].ToString(),
                dec = dt.Rows[0]["dec"].ToString(),
                mrp = Convert.ToInt32(dt.Rows[0]["mrp"].ToString()),
                brand = dt.Rows[0]["brand"].ToString(),
                salerate = Convert.ToInt32(dt.Rows[0]["salerate"].ToString()),
                product_pic = dt.Rows[0]["product_icon"].ToString()
                
            };


            return Json(list, JsonRequestBehavior.AllowGet);

        }


        //binding subcategory

        [HttpPost]
        public ActionResult bind_subcat(int cat_id)
        {
           DataTable dt = dl.SelectSubCategory(cat_id);
            List<subcategory_data> list = new List<subcategory_data>();
            foreach (DataRow i in dt.Rows)
            {
                list.Add(new subcategory_data()
                {
                    subcat_id = Convert.ToInt32(i["subcat_id"]),
                    cat_id = Convert.ToInt32(i["cat_id"]),
                    subcat_name = i["subcat_name"].ToString()
                  
                });

            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }



       
       


    }
}