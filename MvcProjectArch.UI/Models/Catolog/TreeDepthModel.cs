using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class TreeDepthModel
    {
        public int TreeSize { get; set; }
        public int[] DepthItemCount { get; set; }
        public int Depth { get; set; }
        public string CategoryName { get; set; }
    }
}