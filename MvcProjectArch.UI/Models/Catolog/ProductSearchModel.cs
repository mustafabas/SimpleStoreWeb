using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductSearchModel
    {
        public ProductSearchModel()
        {
            this.Products=new List<ProductItemModel>();
        }
        public string ProductGroupName { get;set;}
        public string BrandName { get;set;}
        public string ModelName { get; set; }
        public List<ProductItemModel> Products { get;set;}
    }
}