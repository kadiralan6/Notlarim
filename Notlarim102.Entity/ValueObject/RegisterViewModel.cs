using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim102.Entity.ValueObject
{
    public class RegisterViewModel
    {
        //20//12/5.
        [DisplayName("Kullanici Adi"), Required(ErrorMessage = "{0} alani boş geçilemezz."), StringLength(30, ErrorMessage = "{0} max. {1} olmali ")]
        public string UserName { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} alani boş geçilemezz."), StringLength(100, ErrorMessage = "{0} max. {1} "),EmailAddress(ErrorMessage ="{0} alani için geçerli bir mail adresi giriniz")]
        public string Email { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} alani boş geçilemezz."), DataType(DataType.Password), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmali")]
        public string Password { get; set; }
        [DisplayName("Sifre Tekrar"), Required(ErrorMessage = "{0} alani boş geçilemezz."), DataType(DataType.Password), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmali"), Compare("Password",ErrorMessage = "{0} ile {1} uyuşmuyor ")]
        public string RePassword { get; set; }
    }
}