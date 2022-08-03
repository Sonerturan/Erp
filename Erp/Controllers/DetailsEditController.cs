using Dapper;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.Controllers
{
    public class DetailsEditController : Controller
    {
        // GET: DetailsEdit
        public ActionResult DetailsEdit()
        {
            if (Request.Cookies["Username"] != null)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserName", Request.Cookies["Username"].Value);
                return View(DapperORM.ReturnList<Details_Edit>("EmployeeViewUserName", param).FirstOrDefault<Details_Edit>());
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DetailsEdit(Models.Details_Edit details_Edit)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", details_Edit.Name);
                param.Add("@UserName", Request.Cookies["Username"].Value);
                param.Add("@Mail", details_Edit.Mail);
                param.Add("@Password", details_Edit.Password);
                DapperORM.ExecuteWithoutReturn("EmployeeViewAll", param);

                Response.Cookies["Signin"].Expires = DateTime.Now.AddDays(-1);
                Request.Cookies.Remove("Signin");

                HttpCookie Cookie1 = new HttpCookie("logoutalert");
                Cookie1["logoutalertvalue"] = "a";
                Cookie1.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Cookie1);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}