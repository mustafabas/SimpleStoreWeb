using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class PrepareProductDescriptionTable
    {
        public Hashtable ProductDescriptionTable { get; set; }
        public PrepareProductDescriptionTable()
        {
            this.ProductDescriptionTable = new Hashtable();
        }
        public void CreateProductDescriptionTable(List<ProductItemModel> Products)
        {
            foreach (var item in Products)
            {
                
            }     
        }
    }
}