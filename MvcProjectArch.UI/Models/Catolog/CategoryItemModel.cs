using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class CategoryItemModel
    {
        public int ID { get; set; }
        public int CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public string Url { get; set; }
        public int CategoryType { get; set; }
    }
}