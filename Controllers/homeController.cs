using project_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace project_Ecommerce.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        datalayer dl = new datalayer();
        dblayer db = new dblayer();
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                DataTable dt3 = dl.SelectAllcategories();
                DataTable dt = dl.SelectSubCategory(Convert.ToInt32(id));
                DataTable dt2 = dl.SelectProductOfOneCategory(User.Identity.Name,Convert.ToInt32(id));
                ViewBag.products = dt2;
                ViewBag.subcategory = dt;
                return View(dt3);
            }
            else
           return RedirectToAction("product_category");
        }
        public ActionResult product_category()
        {
            DataTable dt = dl.SelectAllcategories();
            DataTable dt2 = dl.SelectDiscountedProduct(User.Identity.Name);
            ViewBag.products = dt2;
            Session["cartitem"]= dl.GetCartItem(User.Identity.Name);
            return View(dt);
        }

        public ActionResult registration()
        {
            return View();
        }

        // register new user 
        [HttpPost]
        public ActionResult add_registration(reg_data r)
        {
            object a;
            if (r.pass == r.pass1)
            {
                SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("action",1),
            new SqlParameter("name",r.name),
            new SqlParameter("email",r.email),
            new SqlParameter("mobno",r.mobno),
            new SqlParameter("pass",r.pass),
            new SqlParameter("address",r.address),
            new SqlParameter("pincode",r.pincode),
            new SqlParameter("landmark",r.landmark),
            new SqlParameter("user_pic",r.user_pic.FileName)

            };
                a = db.ExcuteScalar("sp_reg", sp);
                if (a != null)
                {
                    r.user_pic.SaveAs(Server.MapPath("/content/categoryicon/user_pic") + r.user_pic.FileName);
                    string message = "Welcome To, <h3 style='color:red'>Techpile Technology Pvt. Ltd.</h3><br/> <br/> Your Login ID is :  "+r.email+"<br/>Your Password is :"+r.pass+"<br/><br/>We are happy to serve you.";
                    dl.SendEmail(r.email,message);
                }
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("<script>alert('Password Matched successfully');window.location.href='/home/registration'</script>");
            }

        }

        //update modal data into form 
        [HttpPost]
        public ActionResult updatemodal(reg_data r)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("action",3),
           
            new SqlParameter("mobno",r.mobno),
           
            new SqlParameter("address",r.address),
            new SqlParameter("pincode",r.pincode),
            new SqlParameter("landmark",r.landmark)
           

            };
            int q = db.ExcuteDML("sp_reg", sp);

            return Json("Address Update Successfully!!", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost,AllowAnonymous]
        public ActionResult login(login_data l)
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("mobno",l.mobno),
            new SqlParameter("pass",l.pass)
            };
            DataTable dt = db.ExcuteGetTable("sp_login", sp);
            if (dt.Rows.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(l.mobno, false);              
                return RedirectToAction("product_category", "home");
            }
            else
            {
                ViewBag.error = "login unsuccessful";
                return View();
            }
        }

        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("cartitem");
            return RedirectToAction("product_category");           
        }

        [HttpPost]
        public ActionResult AddToCart(int product_id, int quantity)
        {
            if (User.Identity.IsAuthenticated)
            {
                string mobile = User.Identity.Name;
                int r = dl.AddToCart(product_id, mobile, quantity);
                Session["cartitem"] = dl.GetCartItem(User.Identity.Name);
                return Json(Session["cartitem"], JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public ActionResult cart()
        {

            DataTable dt = dl.SelectAllcategories();
            DataTable dt1 = dl.GetCartItems(User.Identity.Name);
            DataTable dt2 = dl.GetuserInfo(User.Identity.Name);
            ViewBag.user = dt2;
            ViewBag.cart = dt1;
            return View(dt);
        }
        [Authorize]
        public ActionResult success_order()
        {
            
            DataTable dt = dl.GetCartItems(User.Identity.Name);
            DataTable dt2 = dl.GetuserInfo(User.Identity.Name);
            ViewBag.user = dt2;
            ViewBag.order = dt;
            return View();
        }

    }

    
}