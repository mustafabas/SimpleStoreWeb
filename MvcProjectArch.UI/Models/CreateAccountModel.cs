using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Models
{
    public class CreateAccountModel
    {
        public CreateAccountModel()
        {
            this.Cities = new List<SelectListItem>();
            this.Occupations = new List<SelectListItem>();
        }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Gender { get; set; }
        public string Age { get; set; }
        public string Salary { get; set; }
        public string Condition { get; set; }
        public string Occupation { get; set; }
        public string City { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Occupations { get; set; }
        public bool? Created { get; set; }
    }
}