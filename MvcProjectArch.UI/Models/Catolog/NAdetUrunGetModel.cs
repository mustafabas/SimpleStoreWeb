using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class NAdetUrunGetModel
    {
        public NAdetUrunGetModel()
        {
            this.Products = new List<ProductItemModel>();
        }
        public List<ProductItemModel> Products { get; set; }
        public string CategoryName { get; set; }
        public int Coun { get; set; }
        
    }
}