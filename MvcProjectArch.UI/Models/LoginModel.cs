using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set;}
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }
    }
}