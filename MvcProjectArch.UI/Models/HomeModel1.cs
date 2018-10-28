using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class HomeModel1
    {
        public HomeModel1()
        {
            this.Products = new List<ProductItemModel>();
            this.CatgoryModel = new CategoryItemModel();

        }
       public  List<ProductItemModel> Products { get; set; }

       public CategoryItemModel CatgoryModel { get; set; }
    }
}