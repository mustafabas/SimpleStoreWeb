using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductGroup
    {
        public ProductGroup()
        {
            this.Products = new List<ProductItemModel>();
        }
        public int CategoryId { get; set; }
        public string ProductGroupName { get; set; }
        public List<ProductItemModel> Products { get; set; }
      
    }
}