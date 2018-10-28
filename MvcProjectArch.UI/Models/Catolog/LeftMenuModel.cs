using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class LeftMenuModel
    {
        public LeftMenuModel()
        {
            this.LeftMenuTop = new List<LeftMenuItemModel>();

        }
        public List<LeftMenuItemModel> LeftMenuTop { get; set; }
    }
    public class LeftMenuItemModel
    {
        public LeftMenuItemModel()
        {
            this.SubMenu = new List<LeftMenuItemModel>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public List<LeftMenuItemModel> SubMenu { get; set; }
       
    }
}