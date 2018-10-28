using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductItemModel
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string ModelName { get; set; }
        public int ProductNumber { get; set; }
        public decimal ProductCost { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductDescription { get; set; }



    }
}