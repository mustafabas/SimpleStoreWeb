using MvcProjectArch.Services.Users;
using MvcProjectArch.UI.Models;
using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IUserService  _userService;
        
        public AccountController(IUserService  userService)
        {
            this._userService = userService;
        }
        public ActionResult Register(string message)
        {
          
            List<SelectListItem> cities = new List<SelectListItem>();
            cities.Add(new SelectListItem {Text="İstanbul",Value="İstanbul",Selected=true });
            cities.Add(new SelectListItem { Text = "İzmir", Value = "İzmir" });
            cities.Add(new SelectListItem { Text = "Ankara", Value = "Ankara" });
            cities.Add(new SelectListItem { Text = "Bursa", Value = "Bursa" });
            cities.Add(new SelectListItem { Text = "Sakarya", Value = "Sakarya" });
            cities.Add(new SelectListItem { Text = "Edirne", Value = "Edirne" });
            cities.Add(new SelectListItem { Text = "Tekirdağ", Value = "Tekirdağ" });
            cities.Add(new SelectListItem { Text = "Antalya", Value = "Antalya" });
            cities.Add(new SelectListItem { Text = "Diğer", Value = "Diğer" });

            var occupations = new List<SelectListItem>();
            occupations.Add(new SelectListItem { Text = "Bilgisayar Mühendisi", Value = "Bilgisayar Mühendisi",Selected=true});
            occupations.Add(new SelectListItem { Text = "Elektronik Mühendisi", Value = "Elektronik Mühendisi"});
            occupations.Add(new SelectListItem { Text = "Bankacı", Value = "Bankacı" });
            occupations.Add(new SelectListItem { Text = "Doktor", Value = "Doktor" });
            occupations.Add(new SelectListItem { Text = "İnşaat Mühendisi", Value = "İnşaat Mühendisi" });
            occupations.Add(new SelectListItem { Text = "Makina Mühendisi", Value = "Makina Mühendisi" });
            occupations.Add(new SelectListItem { Text = "Diğer", Value = "Diğer" });


            CreateAccountModel model = new CreateAccountModel();
            if (!string.IsNullOrEmpty(message))
            {
                model.Created = true;
            }
            model.Cities = cities;
            model.Occupations = occupations;
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(CreateAccountModel model)
        {
            Customer customer = new Customer();
            customer.NameSurname = model.NameSurname;
            customer.UserName = model.UserName;
            customer.Salary = Convert.ToDecimal(model.Salary);
            customer.Email = model.Email;
            customer.Age = model.Age;
            customer.Password = model.Password;
            customer.City = model.City;
            customer.Occupation = model.Occupation;

            if(model.Condition=="1")
            {
                customer.Condition = true;
            }
            else
            {
                customer.Condition = false;
            }
            customer.Gender = Convert.ToByte(model.Gender);
            _userService.AddCustomer(customer);
            return RedirectToAction("Register", new {message="success" });

        }
    }
}