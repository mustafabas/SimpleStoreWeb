using MvcProjectArch.Entities.Tables.Catolog;
using MvcProjectArch.Services.Catolog;
using MvcProjectArch.UI.Models;
using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
    [Authorize]
    public class CategoryManagmentController : BaseController
    {
        ICategoryService _categoryService;
        public CategoryManagmentController(ICategoryService categoryService)
        {
    
        
            _categoryService = categoryService;

        }
        public ActionResult Index(int? categoryId, string addedCat)//verileri listelerken entity framework kullanılmıştır.
        {

            string role = CurrentUser.Role;
            if(role!=Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index","home");
            }
            CategoryModel model = new CategoryModel();


            if (categoryId != null)
            {
                int categoryParentId = Convert.ToInt32(categoryId);
                model.SelectedCategoryName = _categoryService.GetCategoryByCategoryId(categoryParentId).CategoryName;
                var categories = _categoryService.GetCategoriesByCategoryParentId(categoryParentId);
                foreach (var item in categories)
                {
                    model.Categories.Add(new CategoryItemModel { CategoryName = item.CategoryName, ID = item.ID, CategoryParentId = item.CategoryParentId, CategoryType = item.CategoryType });
                }
            }
            else
            {
                var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
                foreach (var item in categories)
                {
                    model.Categories.Add(new CategoryItemModel { CategoryName = item.CategoryName, ID = item.ID, CategoryParentId = item.CategoryParentId, Url = "/categorymanagment/index?categoryId=" + item.ID });
                }
            }
            if(!string.IsNullOrEmpty(addedCat))
            {
                model.SuccessMessage = "Ekleme işlemi yapılmıştır";
            }
            return View(model);
        }
        public ActionResult Delete(int CategoryId)
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
            var category = _categoryService.GetCategoryByCategoryId(CategoryId);
            if(category!=null)
            { 
            _categoryService.DeleteCategory(category);
            return RedirectToAction("index", new { check = true });
            }
            else
            {
                return RedirectToAction("index", new { check = false });
            }
           
        }
        public ActionResult Add(int? categoryParentId)
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
            CategoryAddModel category = new CategoryAddModel();
            if (categoryParentId != null)
            {
                category.CategoryType = (byte)CategoryType.ProductGroup;
                var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
                foreach (var item in categories)
                {
                    category.TopCategories.Add(new SelectListItem {Text=item.CategoryName,Value=item.ID.ToString() });
                }
            }
            else category.CategoryType = (byte)CategoryType.Category;
            return View(category);
        }
        [HttpPost]
        public ActionResult Add(CategoryAddModel model)
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
            var category = new Category();
            category.CategoryName = model.CategoryName;
            if(model.CategoryParentId!=null)
            {
                category.CategoryParentId = model.CategoryParentId;
                category.CategoryType = (byte)CategoryType.ProductGroup;
            }
            else
            {
                category.CategoryType = (byte)CategoryType.Category;
            }
            category.Status = true;
            _categoryService.AddCategory(category);
            if (model.CategoryParentId != null)
            {
                return RedirectToAction("index", new { categoryId = model.CategoryParentId, addedCat = "true" });
            }
            else
            {
                return RedirectToAction("index", new {addedCat="true"});
            }
        }
        [HttpPost]
        public JsonResult GetProductGroup(int categoryId)
        {
            var categoryProductGroup = _categoryService.GetCategoriesByCategoryParentId(categoryId);
            List<CategoryItemModel> itemModel = new List<CategoryItemModel>();
            foreach (var item in categoryProductGroup)
            {
                itemModel.Add(new CategoryItemModel {ID=item.ID,CategoryName=item.CategoryName });
            }
            return Json(itemModel, JsonRequestBehavior.AllowGet);
        }


  
    }
}