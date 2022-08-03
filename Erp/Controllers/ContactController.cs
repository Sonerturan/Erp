using Erp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Erp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(MailSend mailSend)
        {
            if (ModelState.IsValid)
            {
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

                if (sc.EnableSsl == true)
                {
                    sc.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"], ConfigurationManager.AppSettings["MailPassword"]);
                    MailMessage mail = new MailMessage
                    { From = new MailAddress(ConfigurationManager.AppSettings["MailUserName"], "ERP Web Site") };

                    mail.To.Add("sonerturan609@gmail.com");

                    mail.Subject = "Kullanıcıdan Gelenler";

                    mail.Body = ""+
                        "<img src=https://instagram.fada1-5.fna.fbcdn.net/vp/2059662cf1e076c5498e51dae545d263/5DCDE112/t51.2885-15/sh0.08/e35/s640x640/66817187_446035359585740_7528445371196118745_n.jpg?_nc_ht=instagram.fada1-5.fna.fbcdn.net>" +
                        "<div class=container>" +

                        "<div>"+
                        "<div class=col-md-7>"+
                        "<h2><u> Web ERP Gelen Kutusu </u></h2>"+
                        "</div>"+
                        "</div>"+

                        "<div>" +
                        "<div class=col-md-8 form_side>" +
                        "<h3>Konu : <i>" + mailSend.Subject + " </i> "+"</h3>" +
                        "</div>" +
                        "</div>" +

                        "<div>" +
                        "<div class=col-md-8 form_side>" +
                        "<h3>Mesaj : </h3>" +
                        "</div>" +
                        "</div>" +
                        "<div>" +
                        "<div class=col-md-8 form_side>" +
                        "<h3> * <i>" + mailSend.Body + " </i>  * " + "</h3>" +
                        "</div>" +
                        "</div>" +

                        "<div>" +
                        "<div class=col-md-8 form_side>" +
                        "<h4>Gönderen : " + mailSend.EmailAdress + "</h4>" +
                        "</div>" +
                        "</div>" +

                        "</div>" +

                        "<img src=https://instagram.fada1-5.fna.fbcdn.net/vp/c07375aefbf4500b665b7ab3a31f7344/5DE42DBE/t51.2885-15/sh0.08/e35/s640x640/67113392_915314738810616_8212263935286307859_n.jpg?_nc_ht=instagram.fada1-5.fna.fbcdn.net>"
                        ;


                    mail.IsBodyHtml = true;
                    sc.Send(mail);

                    //sweet alert için cookies
                    HttpCookie Cookie3 = new HttpCookie("MailSend");
                    Cookie3["Send"] = "1";
                    Cookie3.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(Cookie3);

                }
                
                return RedirectToAction("Contact", "Contact");
            }
            return View();
        }
    }
}
