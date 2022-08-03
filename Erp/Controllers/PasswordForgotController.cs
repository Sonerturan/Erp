using Dapper;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Erp.Controllers
{
    public class PasswordForgotController : Controller
    {
        // GET: PasswordForgot
        public ActionResult EnterUserName()
        {
            if (Request.Cookies["passwordforget"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EnterUserName(User user)
        {
            using (var connection = new SqlConnection("Data Source = .; Initial Catalog= MVCDapperCrudDB; Integrated Security = True"))
            {
                var employee = connection.QueryFirstOrDefault("select * from Employee where UserName=@UserName", new { UserName = user.UserName });
                var mailsend = connection.QueryFirstOrDefault("select Mail from Employee where UserName=@UserName", new { UserName = user.UserName });
                user.Mail = mailsend.Mail.ToString();
                if (employee != null)
                {
                    HttpCookie Cookie4 = new HttpCookie("useruser")
                    {
                        Value = user.UserName,
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Response.Cookies.Add(Cookie4);

                    Random r = new Random();
                    int num = r.Next(999999999);
                    
                    HttpCookie Cookie5 = new HttpCookie("ConfirmationCode")
                    {
                        Value = num.ToString(),
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Response.Cookies.Add(Cookie5);

                    

                    /////////////////
                    SmtpClient sc = new SmtpClient
                    {
                        Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]),
                        Host = ConfigurationManager.AppSettings["MailServer"]
                    };
                    if (ConfigurationManager.AppSettings["SSL"] == "1")
                    {
                        sc.EnableSsl = true;
                    }
                    else
                    {
                        sc.EnableSsl = false;
                    }

                    sc.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                    MailMessage mail = new MailMessage
                    { From = new MailAddress(ConfigurationManager.AppSettings["MailUserName"], "ERP Web Site") };

                    mail.To.Add(user.Mail);

                    mail.Subject = "Hesabınızı Kurtarın";

                    mail.Body = "" +
                        "<img src=https://instagram.fada1-5.fna.fbcdn.net/vp/2059662cf1e076c5498e51dae545d263/5DCDE112/t51.2885-15/sh0.08/e35/s640x640/66817187_446035359585740_7528445371196118745_n.jpg?_nc_ht=instagram.fada1-5.fna.fbcdn.net>" +
                        "<div class=container>" +

                        "<div>" +
                        "<div class=col-md-7>" +
                        "<h2><u> Onay Kodu </u></h2>" +
                        "</div>" +
                        "</div>" +

                        "<br/>" +

                        "<div>" +
                        "<div class=col-md-8 form_side>" +
                        "<h2>" + num + "</h2>" +
                        "</div>" +
                        "</div>" +



                        "</div>" +

                        "<img src=https://instagram.fada1-5.fna.fbcdn.net/vp/c07375aefbf4500b665b7ab3a31f7344/5DE42DBE/t51.2885-15/sh0.08/e35/s640x640/67113392_915314738810616_8212263935286307859_n.jpg?_nc_ht=instagram.fada1-5.fna.fbcdn.net>"
                        ;


                    mail.IsBodyHtml = true;
                    sc.Send(mail);
                    /////////////////

                    return RedirectToAction("PasswordForgot", "PasswordForgot");
                }
                else
                {
                    ViewBag.UserError = "Kullanıcı Adı Hatalı";
                    return View();
                }
            }
        }

        public ActionResult PasswordForgot()
        {
            if (Request.Cookies["passwordforget"] != null)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", Request.Cookies["useruser"].Value);
                return View(DapperORM.ReturnList<User>("EmployeeViewMail", param).FirstOrDefault<User>());
            }

            return View();
        }

        [HttpPost]
        public ActionResult PasswordForgot(User user)
        {
            string num = Request.Cookies["ConfirmationCode"].Value;
            if (user.ConfirmationCode == num)
            {
                return RedirectToAction("EnterPassword", "PasswordForgot");
            }

            ViewBag.ConfirmationCode = "Onay Kodunu Yanlış Girdiniz";

            return View();
        }
        public ActionResult EnterPassword()
        {
            if (Request.Cookies["passwordforget"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EnterPassword(PasswordForgot passwordForgot)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", Request.Cookies["Username"].Value);
                param.Add("@Password", passwordForgot.Password);
                DapperORM.ExecuteWithoutReturn("EmployeeViewPasswordForgot", param);

                Response.Cookies["Signin"].Expires = DateTime.Now.AddDays(-1);
                Request.Cookies.Remove("Signin");

                HttpCookie Cookie1 = new HttpCookie("logoutalert");
                Cookie1["logoutalertvalue"] = "a";
                Cookie1.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Cookie1);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}