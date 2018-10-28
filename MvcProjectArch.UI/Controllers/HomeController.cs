using Microsoft.AspNet.Identity;
using MvcProjectArch.Entities.Tables.Catolog;
using MvcProjectArch.Services.Catolog;
using MvcProjectArch.UI.AuthHelper;
using MvcProjectArch.UI.Models;
using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
     [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(ICategoryService categoryService,IProductService productService)
        {
      
            
            _categoryService = categoryService;
            _productService = productService;
        }
        // GET: Home
    
        public ActionResult Index()
        {
            //ViewBag.ID = CurrentUser.Role;//
            if(User.Identity.IsAuthenticated)
            {
                string role = CurrentUser.Role;
                if (role == Convert.ToString((byte)UserType.Admin))
                {
                    return View("IndexAdmin");
                }
            }
                var products=_productService.GetAllProduct();
                HomeModel1 model = new HomeModel1();
                foreach (var item in products)
                {
                    model.Products.Add(new ProductItemModel {ModelName=item.ModelName,ProductPrice=item.ProductPrice,ProductId=item.ID,CategoryName=item.Category.CategoryName,ProductDescription=item.ProductDescription,ProductName=item.ProductName });
                }
                return View(model);
            
        }
         [HttpGet]
         public ActionResult ProductCategorie(string productGoupName,string PriceBegin,string PriceLast)
        {
            HomeModel1 model = new HomeModel1();
            ViewBag.CategoryName = productGoupName;
            ViewBag.PriceBegin = PriceBegin;
            ViewBag.PriceLast = PriceLast;
            var categories = _categoryService.GetAllCategory();
            PrepareCategoryHash categoryHash = new PrepareCategoryHash(); //kategori ağaç class
            categoryHash.CreateHashTableAndProductTree();//kategori ve ürün agacını oluştur
            Hashtable hashTable = categoryHash.GetCategoryTable;
            if (string.IsNullOrEmpty(PriceBegin))
             {
         
                 List<ProductItemModel> list = new List<ProductItemModel>();
                 var category = _categoryService.GetAllCategory().Where(x => x.CategoryName == productGoupName).FirstOrDefault();
                 var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId);

                 ProductGroupTree treeGroup = (ProductGroupTree)hashTable[categoryParent.CategoryName];//product group agacı tipine casting işlemi
                 var treeNode = treeGroup.GetProductTreeNodeByProductGroupName(treeGroup.GetRoot(), productGoupName);
                 if (treeNode != null)
                 {
                     model.Products.AddRange(treeNode.ProductBase.Products);
                 }
             }
             else
             {
                 decimal priceBegin=Convert.ToDecimal(PriceBegin);
                 decimal priceLast=Convert.ToDecimal(PriceLast);
                if(string.IsNullOrEmpty(productGoupName))
                {
                    var categories1 = _categoryService.GetAllCategory().Where(x=>x.CategoryType==(byte)CategoryType.Category);
                    foreach (var item in categories1)
                    {
                        ProductGroupTree treeGroup = (ProductGroupTree)hashTable[item.CategoryName];
                        treeGroup.GetAllProductPriceBetween(treeGroup.GetRoot(), priceBegin, priceLast);
                        model.Products.AddRange(treeGroup.ProductsBetween);
                        
                    }

                }
                else
                {
                   
               
                    List<ProductItemModel> list = new List<ProductItemModel>();
                    var category = _categoryService.GetAllCategory().Where(x => x.CategoryName == productGoupName).FirstOrDefault();
                    var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId);

                    ProductGroupTree treeGroup = (ProductGroupTree)hashTable[categoryParent.CategoryName];//product group agacı tipine casting işlemi
                    var treeNode = treeGroup.GetProductTreeNodeByProductGroupName(treeGroup.GetRoot(), productGoupName);
                    if (treeNode != null)
                    {
                        foreach (var item in treeNode.ProductBase.Products.ToList())
                        {
                            if(item.ProductPrice>priceBegin && item.ProductPrice<priceLast)
                            {
                                model.Products.AddRange(treeNode.ProductBase.Products);
                            }
                        }
                    }
                }

             }
           
            

            return View(model);
        }
    
        public PartialViewResult GetLeftMenuShared()
        {
            LeftMenuModel model = new LeftMenuModel();
            var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
            PrepareCategoriesMenu(model,categories);


            return PartialView(model);
        }
        public void PrepareCategoriesMenu(LeftMenuModel model,IEnumerable<Category> categories)
        {
           
            foreach (var item in categories)
            {
                LeftMenuItemModel itemModel = new LeftMenuItemModel();
                itemModel.CategoryName = item.CategoryName;
                var subCategory = _categoryService.GetSubCategoriesByCategoryId(item.ID);
                foreach (var itemSub in subCategory)
                {
                    itemModel.SubMenu.Add(new LeftMenuItemModel {CategoryName=itemSub.CategoryName });
                }
                model.LeftMenuTop.Add(itemModel);
            }
        }
         [HttpGet]
         public ActionResult TraverByType(string type)
        {
        
            var categories = _categoryService.GetAllCategory();
            PrepareCategoryHash categoryHash = new PrepareCategoryHash(); //kategori ağaç class
            categoryHash.CreateHashTableAndProductTree();//kategori ve ürün agacını oluştur
            Hashtable hashTable = categoryHash.GetCategoryTable;
            List<ProductManagmentHomeModel> model = new List<ProductManagmentHomeModel>();
            ViewBag.type = type;
            List<ProductGroupTreeNode> treeNode1 = new List<ProductGroupTreeNode>();
            foreach (var item in categories) //ürün ağacından verileri modele atama
            {
                ProductGroupTree treeGroup = (ProductGroupTree)hashTable[item.CategoryName];//product group agacı tipine casting işlemi


                if (treeGroup != null)
                {
                    if(type=="inorder")
                    {
                        treeGroup.inOrder(treeGroup.GetRoot());
                    }
                    else if(type=="preorder")
                    {
                        treeGroup.preOrder(treeGroup.GetRoot());
                    }
                    else if(type=="postorder")
                    {
                        treeGroup.postOrder(treeGroup.GetRoot());
                    }
                   //urun grubu ağacı inorder dolanır
                    var treeNode = treeGroup.ProductNode;
                   
                    treeNode1.AddRange(treeNode);
               
                }
            }

            return View(treeNode1);
        }
         public ActionResult TreeDepth()
         {
             PrepareCategoryHash categoryHash = new PrepareCategoryHash();
             categoryHash.CreateHashTableAndProductTree();
             var categories = _categoryService.GetAllCategory().Where(x=>x.CategoryType==(byte)CategoryType.Category);
             List<TreeDepthModel> model = new List<TreeDepthModel>();
             foreach (var item in categories)
             {
                 var productGroupTree = (ProductGroupTree)categoryHash.GetCategoryTable[item.CategoryName];
                 TreeDepthModel s = new TreeDepthModel();
                 productGroupTree.findTreeInfo(productGroupTree.GetRoot(),productGroupTree.TreeSize);
                 s.Depth = productGroupTree.maxDepth;
                 s.DepthItemCount=productGroupTree.elementCountForEachDepth;
                 s.TreeSize = productGroupTree.TreeSize;
                 s.CategoryName = item.CategoryName;
                 model.Add(s);


             }
             return View(model);

         }
    }
}