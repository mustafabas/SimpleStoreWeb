using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductAddModel
    {
        public ProductAddModel()
        {
            this.Categories = new List<SelectListItem>();
            this.ProductGroups = new List<SelectListItem>();
        }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public string Categoryame { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public string ProductGroupName { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> ProductGroups { get; set; }
        public decimal Cost{ get; set; }
        public int ProductNumber { get; set; }
        public int ProductGroupId { get; set; }
        public bool ProductAddedCheck{ get; set; }
        public int ProductId { get; set; }
    }
}