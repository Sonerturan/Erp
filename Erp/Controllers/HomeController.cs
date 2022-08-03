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
    public class HomeController : Controller
    {
        public ActionResult Index(User user)
        {

            if (Request.Cookies["Signin"] != null)
            {
                using (var connection = new SqlConnection("Data Source = .; Initial Catalog= MVCDapperCrudDB; Integrated Security = True"))
                {
                    user.ThemeDark = 1;
                    user.UserName = Request.Cookies["Username"].Value;
                    var employee = connection.QueryFirstOrDefault("select * from Employee where UserName=@UserName and ThemeDark=@ThemeDark", new { UserName = user.UserName , ThemeDark = user.ThemeDark });
                    if (employee != null)
                    {
                        HttpCookie Cookie1 = new HttpCookie("Dark2");
                        Cookie1["kullaniciAdi"] = "a";
                        Cookie1.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(Cookie1);

                        return View();
                    }
                    else
                    {
                        Response.Cookies["Dark2"].Expires = DateTime.Now.AddDays(-1);
                        Request.Cookies.Remove("Dark2");
                        return View();
                    }
                }
            }

            Response.Cookies["Dark2"].Expires = DateTime.Now.AddDays(-1);
            Request.Cookies.Remove("Dark2");
            return View();
        }
        public ActionResult CookieDelete()
        {
            Response.Cookies["Signin"].Expires = DateTime.Now.AddDays(-1);

            return Redirect("Index");
        }
        public ActionResult ThemeDark(User user)
        {
            user.ThemeDark = 1;

            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", Request.Cookies["Username"].Value);
            param.Add("@ThemeDark", user.ThemeDark);
            DapperORM.ExecuteWithoutReturn("EmployeeAddTheme", param);

            return Redirect("Index");
        }
        public ActionResult NotThemeDark(User user)
        {
            user.ThemeDark = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", Request.Cookies["Username"].Value);
            param.Add("@ThemeDark", user.ThemeDark);
            DapperORM.ExecuteWithoutReturn("EmployeeAddTheme", param);

            Response.Cookies["Dark2"].Expires = DateTime.Now.AddDays(-1);
            Request.Cookies.Remove("Dark2");

            return Redirect("Index");
        }

    }
}