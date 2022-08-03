using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.Models
{
    public class MailSend
    {
        [EmailAddress]
        [DisplayName("Email Adresiniz")]
        [Required(ErrorMessage = "Email Adresi Boş Geçilemez!")]
        public string EmailAdress { get; set; }

        [DisplayName("Konu")]
        [Required(ErrorMessage = "Konu Alanı Boş Geçilemez")]
        public string Subject { get; set; }


        [DisplayName("Mesaj")]
        [Required(ErrorMessage = "Mesaj Alanı Boş Geçilemez")]
        public string Body { get; set; }
    }
}