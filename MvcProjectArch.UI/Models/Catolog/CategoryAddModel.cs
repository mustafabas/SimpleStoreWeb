using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class CategoryAddModel
    {
        public CategoryAddModel()
        {
            this.TopCategories = new List<SelectListItem>();
        }
        public string CategoryName { get; set; }
        public List<SelectListItem> TopCategories{get;set;}
        public int CategoryType { get; set; }
        public int CategoryParentId { get; set; }

    }

}