using Dapper;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection("Data Source = .; Initial Catalog= MVCDapperCrudDB; Integrated Security = True"))
                {
                    var employee = connection.QueryFirstOrDefault("select * from Employee where UserName=@UserName", new { UserName = employeeModel.UserName });
                    if (employee != null)
                    {
                        ViewBag.LoginError2 = "Bu Kullanıcı Adı Zaten Mevcut!";
                        return View();
                    }
                    else
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@EmployeeID", employeeModel.EmployeeID);
                        param.Add("@Name", employeeModel.Name);
                        param.Add("@UserName", employeeModel.UserName);
                        param.Add("@Mail", employeeModel.Mail);
                        param.Add("@Password", employeeModel.Password);
                        DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);

                        //Cookie oluşturuyoruz.
                        HttpCookie Cookie = new HttpCookie("Register");
                        Cookie["kullaniciAdi"] = "asasasass";
                        Cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(Cookie);
                        
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return View();
            }
        }
        
    }
}