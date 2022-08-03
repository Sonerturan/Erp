using Dapper;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.Controllers
{
    public class Sign_inController : Controller
    {
        // GET: Sign_in
        public ActionResult Sign_in()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Sign_in(User user)
        {
            if (ModelState.IsValid)
            {

                using (var connection = new SqlConnection("Data Source = .; Initial Catalog= MVCDapperCrudDB; Integrated Security = True"))
                {
                    var employee = connection.QueryFirstOrDefault("select * from Employee where Password=@Password and UserName=@UserName", new { Password = user.Password , UserName = user.UserName });
                    if (employee != null)
                    {
                        HttpCookie Cookie = new HttpCookie("Signin")
                        { Expires = DateTime.Now.AddDays(1)};
                        Cookie["Veri2"] ="1";
                        Response.Cookies.Add(Cookie);

                        HttpCookie musteriCookie = new HttpCookie("Username")
                        {
                            Value = user.UserName,
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Add(musteriCookie);


                        HttpCookie Cookie1 = new HttpCookie("welcomealert")
                        {
                            Value = user.UserName,
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Add(Cookie1);



                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        HttpCookie Cookie3 = new HttpCookie("passwordforget")
                        {
                            Value = "w",
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Add(Cookie3);

                        ViewBag.LoginError = "Kullanıcı Adı veya Şifre Hatalı";
                        return View();
                    }

                }
            }
            else
            {
                Random r = new Random();
                int num = r.Next(999999999);

                HttpCookie Cookie3 = new HttpCookie("passwordforget")
                {
                    Value = num.ToString(),
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Add(Cookie3);

                return View();
            }
        }
    }
}