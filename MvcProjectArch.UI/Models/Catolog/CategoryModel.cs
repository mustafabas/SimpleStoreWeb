using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.Categories = new List<CategoryItemModel>();
        }
        public string SelectedCategoryName { get; set; }
        public List<CategoryItemModel> Categories { get; set; }
        public string SuccessMessage { get; set; }
        
    }
}