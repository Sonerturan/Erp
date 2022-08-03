using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.Models
{
    public class Details_Edit
    {


        [DisplayName("Ad")]
        [Required(ErrorMessage = "Ad Girilmesi Zorunludur!")]
        public string Name { get; set; }

        [EmailAddress]
        [DisplayName("Mail")]
        [Required(ErrorMessage = "Mail Alanı Gereklidir!")]
        public string Mail { get; set; }

        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Şifre en az 5 karakter olmalıdır!")]
        [Required(ErrorMessage = "Şifre  Alanı Zorunludur!")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Tekrar Alanı Zorunludur!")]
        [Compare("Password", ErrorMessage = "Şifre ile Şifre Tekrarı Eşleşmiyor!")]
        public string Passwordrepeat { get; set; }

        

    }
}