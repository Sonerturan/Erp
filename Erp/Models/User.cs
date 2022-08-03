using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.Models
{
    public class User
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı Alanı Boş Geçilemez!")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre  Alanı Zorunludur! ")]
        public string Password { get; set; }
        

        public int ThemeDark { get; set; }

        public string ConfirmationCode { get; set; }
        
        
        


    }
}