using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Security;
using System.Security.Authentication;
using System.Web.ModelBinding;
using System.Net.Mail;
using System.Net;

namespace project_Ecommerce.Models
{
    public class datalayer
    {
        //function to select all categories
        dblayer db=new dblayer();

        public DataTable SelectAllcategories()
        {
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter ("action",2)
            };
            DataTable dt = db.ExcuteGetTable("sp_category", sp);
            return dt;
        }


        //function to category wise sub-cate

        public DataTable SelectSubCategory(int cat_id)
        {
            SqlParameter[] sp = new SqlParameter[] {
             new SqlParameter("action",3),
             new SqlParameter("cat_id",cat_id)
            };
            DataTable dt = db.ExcuteGetTable("sp_subcategory", sp);
            return dt;         

        }
            //select max discounted product
        public DataTable SelectDiscountedProduct(string userid)
        {
            SqlParameter[]   sp = new SqlParameter[]
                                {
                new SqlParameter("action",6),
                new SqlParameter("user_id",userid)
                                };
           
            DataTable dt = db.ExcuteGetTable("sp_product", sp);
            return dt;
        }
        //select category wise product list 
        public DataTable SelectProductOfOneCategory(string userid,int cat_id)
        {
            SqlParameter[] sp = new SqlParameter[]
                                {
                new SqlParameter("action",7),
                new SqlParameter("user_id",userid),
                new SqlParameter("cat_id",cat_id)
                                };

            DataTable dt = db.ExcuteGetTable("sp_product", sp);
            return dt;
        }

        //for user info
        public DataTable GetuserInfo(string userid)
        {
            SqlParameter[] sp = new SqlParameter[] {
             new SqlParameter("action",2),
              new SqlParameter("mobno",userid),
            };
            DataTable dt = db.ExcuteGetTable("sp_reg", sp);
            return dt;
            
        }

        public int AddToCart(int product_id,string mobile,int quantity)
        {
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("action",1),
                new SqlParameter("product_id",product_id),
                new SqlParameter("quantity",quantity),
                new SqlParameter("user_id",mobile)
                };
            int r = db.ExcuteDML("sp_cartitem", sp);
            
            
            return r;

        }
        //method to select total item added in cart
        public object GetCartItem(string user_id)
        {
            SqlParameter[] sp = new SqlParameter[] { 
            new SqlParameter ("action",2),
            new SqlParameter("user_id",user_id)
            };
            DataTable dt= db.ExcuteGetTable("sp_cartitem", sp);
            string cart_message ="";
            if (dt.Rows.Count>0)
            {
                cart_message += dt.Rows[0]["quantity"] + "  Items<br/> &#x20B9;" + dt.Rows[0]["total"]; 
            }
            return cart_message;
        }
        //select all cart added items
        public DataTable GetCartItems(string user_id)
        {
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter ("action",3),
            new SqlParameter("@user_id",user_id)};
            DataTable dt = db.ExcuteGetTable("sp_cartitem", sp);
            return dt;
        }
        //Code to send email to the user
        public void SendEmail(string email,string mesagebody)
        {
            //COMPOSE MAIL MESSAGE AND SET ALL PROPERTIES
            MailMessage message = new MailMessage("mauryajugdish456@gmail.com", email);
            message.Subject = "WELCOME MESSAGE FROM RAM";
            message.Body = mesagebody;
            message.IsBodyHtml = true;
            message.Bcc.Add("mauryajugdish@gmail.com");
            //SEND MAILMESSAGE BY USING SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("mauryajagdish456@gmail.com", "oykbpxmqnzmmpxdh");
            smtp.Send(message);
        }
        

    }
}


