using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductGroupTreeNode
    {
        public ProductGroupTreeNode LeftChild { get; set; }
        public ProductGroupTreeNode RightChid { get; set; }
        public ProductGroup ProductBase { get; set; }

    }
}