using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class ProductManagmentHomeModel
    {
        public ProductManagmentHomeModel()
        {
            this.ProductGroupTree = new List<ProductGroupTreeNode>();
        }
        public string CategoryName { get; set; }
        public List<ProductGroupTreeNode> ProductGroupTree { get; set; }
        
    }
}