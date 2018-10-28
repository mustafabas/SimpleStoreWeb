using MvcProjectArch.Services.Catolog;
using MvcProjectArch.Services.Checkouts;
using MvcProjectArch.UI.Models;
using MvcProjectArch.UI.Models.Catolog;
using MvcProjectArh.Entities.Tables.Catolog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
   
    public class ProductManagmentController : BaseController
    {
        IProductService _productservice;
        ICategoryService _categoryService;
        ICheckoutService _checkoutService;
        public ProductManagmentController(IProductService productservice, ICategoryService categoryService, ICheckoutService checkoutService)
        {
            this._productservice = productservice;
            this._categoryService = categoryService;
            this._checkoutService = checkoutService;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
            var categories = _categoryService.GetAllCategory();
            PrepareCategoryHash categoryHash = new PrepareCategoryHash(); //kategori ağaç class
            categoryHash.CreateHashTableAndProductTree();//kategori ve ürün agacını oluştur
            Hashtable hashTable = categoryHash.GetCategoryTable;
            List<ProductManagmentHomeModel> model = new List<ProductManagmentHomeModel>();
            foreach (var item in categories) //ürün ağacından verileri modele atama
            {
                ProductGroupTree treeGroup = (ProductGroupTree)hashTable[item.CategoryName];//product group agacı tipine casting işlemi
            

                if (treeGroup != null)
                {
                    treeGroup.inOrder(treeGroup.GetRoot());//urun grubu ağacı inorder dolanır
                    var treeNode = treeGroup.ProductNode;
                    model.Add(new ProductManagmentHomeModel { ProductGroupTree = treeNode, CategoryName = item.CategoryName });
                }
            }

            return View(model);

          
        }
        public ActionResult AddProduct(string message)
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
         
            var productAddModel = new ProductAddModel();
            if (message != null)
            {
                productAddModel.ProductAddedCheck = true;
            }
            var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
            productAddModel.Categories.Add(new SelectListItem {Text="Seçiniz",Value="0", Selected=true });
            foreach (var item in categories)
            {
                productAddModel.Categories.Add(new SelectListItem {Text=item.CategoryName,Value=item.ID.ToString() });
            }

            return View(productAddModel);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductAddModel model)
        {
            string role = CurrentUser.Role;
            if (role != Convert.ToString((byte)UserType.Admin))
            {
                return RedirectToAction("index", "home");
            }
            #region treeinsert
            var category = _categoryService.GetCategoryByCategoryId(model.CategoryId);//tree insert product
            string productGroupName=_categoryService.GetCategoryByCategoryId(model.ProductGroupId).CategoryName;
            PrepareCategoryHash categoryHash = new PrepareCategoryHash();
            categoryHash.CreateHashTableAndProductTree();
            var categoryTable = categoryHash.GetCategoryTable;
            var productGroupTreeForCategory = (ProductGroupTree)categoryTable[category.CategoryName];
            ProductGroupTreeNode node=productGroupTreeForCategory.GetProductTreeNodeByProductGroupName(productGroupTreeForCategory.GetRoot(),productGroupName);
            ProductItemModel productItem=new ProductItemModel();
            productItem.BrandName=model.BrandName;
            productItem.ModelName=model.ModelName;
            productItem.ProductCost=model.Cost;
            productItem.ProductDescription=model.ProductDescription;
            productItem.ProductPrice=model.Price;
            productItem.Status=true;
            productItem.ProductNumber=model.ProductNumber;
            productItem.ProductName=model.ProductName;
            node.ProductBase.Products.Add(productItem);

            #endregion
            #region databaseinsert
            string fileNameDt = "";
            if (Request.Files.Count > 0)//resim upload
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    fileNameDt = fileName;
                    var path = Path.Combine(Server.MapPath("~/ProductImages/"), fileName);
                    file.SaveAs(path);
                }
            }
            Product product = new Product();//database adding
            product.BrandName = model.BrandName;
            product.CategoryId = model.ProductGroupId;
            product.ModelName = model.ModelName;
            product.ProductCost = model.Cost;
            product.ProductDescription = model.ProductDescription;
            product.ProductName = model.ProductName;
            product.ProductNumber = model.ProductNumber;
            product.ProductPrice = model.Price;
            product.ProductImagePath = fileNameDt;
            product.Status = true;
            _productservice.AddProduct(product);
            #endregion
            return RedirectToAction("AddProduct", new {message="success" });
        }
       [HttpGet]
        public ActionResult ProductSearch(string ProductGroupName,string BrandName,string ModelName)
        {
           ProductSearchModel model=new ProductSearchModel();
           model.ProductGroupName = ProductGroupName;
           model.BrandName = BrandName;
           model.ModelName = ModelName;
            PrepareCategoryHash categoryHash = new PrepareCategoryHash();
            categoryHash.CreateHashTableAndProductTree();
            Hashtable categoryTable = categoryHash.GetCategoryTable;
            var categories = _categoryService.GetAllCategory().Where(x=>x.CategoryType==(byte)CategoryType.Category);
            List<ProductItemModel> productList = new List<ProductItemModel>();
            foreach (var item in categories)
            {
                var tree = (ProductGroupTree)categoryTable[item.CategoryName];
                
                    var treeNode = tree.GetProductTreeNodeByProductGroupName(tree.GetRoot(), ProductGroupName);
                    if (treeNode.ProductBase != null)
                    {
                        var anyProduct = treeNode.ProductBase.Products.Where(x => x.ModelName == ModelName && x.BrandName == BrandName).ToList();
                        if (anyProduct.Count > 0)
                        {
                            productList.AddRange(anyProduct);

                        }
                    }
            }

            model.Products = productList;
            return View(model);

        }
        
       public ActionResult Delete(int id)
       {
           var product = _productservice.GetProductByProductId(id);
           var category = _categoryService.GetCategoryByCategoryId(product.CategoryId);

           #region tree_delete_product
           PrepareCategoryHash categoryHash = new PrepareCategoryHash();
           categoryHash.CreateHashTableAndProductTree();
           Hashtable categoryTable = categoryHash.GetCategoryTable;
           var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
           List<ProductItemModel> productList = new List<ProductItemModel>();
           foreach (var item in categories)
           {
               var tree = (ProductGroupTree)categoryTable[item.CategoryName];

               var treeNode = tree.GetProductTreeNodeByProductGroupName(tree.GetRoot(), category.CategoryName
                   );
               if (treeNode.ProductBase != null)
               {
                   var anyProduct = treeNode.ProductBase.Products.Where(x => x.ModelName == product.ProductName && x.BrandName == product.BrandName).FirstOrDefault();
                   if (anyProduct!=null)
                   {
                       treeNode.ProductBase.Products.Remove(anyProduct);
                   }
               }

           }
           #endregion
           _productservice.DeleteProduct(product);//database delete
           return View();    
       }
       public ActionResult Edit(int id, string message)
       {
           var product = _productservice.GetProductByProductId(id);
           var category1 = _categoryService.GetCategoryByCategoryId(product.CategoryId);
           var productAddModel = new ProductAddModel();
           var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
          
           if(!string.IsNullOrEmpty(message))
           {
               productAddModel.ProductAddedCheck = true;
           }
           foreach (var item in categories)
           {
               if (item.ID == category1.ID)
               {
                   productAddModel.Categories.Add(new SelectListItem { Text = item.CategoryName, Value = item.ID.ToString(),Selected=true });
               }
               else { 
               productAddModel.Categories.Add(new SelectListItem { Text = item.CategoryName, Value = item.ID.ToString() });
               }
           }
           productAddModel.ProductId = id;
           productAddModel.BrandName = product.BrandName;
           productAddModel.Cost = product.ProductCost;
           productAddModel.ModelName = product.ModelName;
           productAddModel.Price = product.ProductPrice;
           productAddModel.ProductName = product.ProductName;
           productAddModel.ProductNumber = product.ProductNumber;
           productAddModel.ProductDescription = product.ProductDescription;

           return View(productAddModel);
       }
        [HttpPost]
       public ActionResult Edit(ProductAddModel model,int hdnCategoryId)
       {
           var product = _productservice.GetProductByProductId(model.ProductId);
           var categoryLast = _categoryService.GetCategoryByCategoryId(product.CategoryId);
           var categoryTop = _categoryService.GetCategoryByCategoryId(categoryLast.CategoryParentId);
            if(hdnCategoryId!=model.ProductGroupId)//kategori ve ürün grubu aynı değilse 
            {
                //önce ağaçdaki ürün bulunur ve silinir daha sonra yeni yerine eklenir
                var newProductGroupName = _categoryService.GetCategoryByCategoryId(model.ProductGroupId).CategoryName;
                #region tree_delete_product 
                PrepareCategoryHash categoryHash = new PrepareCategoryHash();
                categoryHash.CreateHashTableAndProductTree();
                Hashtable categoryTable = categoryHash.GetCategoryTable;
                var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
                List<ProductItemModel> productList = new List<ProductItemModel>();
                
                    var tree = (ProductGroupTree)categoryTable[categoryTop.CategoryName];

                    var treeNode = tree.GetProductTreeNodeByProductGroupName(tree.GetRoot(), categoryLast.CategoryName
                        );
                    if (treeNode.ProductBase != null)
                    {
                        var anyProduct = treeNode.ProductBase.Products.Where(x => x.ProductId==model.ProductId).FirstOrDefault();
                        if (anyProduct != null)
                        {
                            treeNode.ProductBase.Products.Remove(anyProduct);
                            
                        }
                    }

                
                #endregion

                #region treeinsert
                var category = _categoryService.GetCategoryByCategoryId(model.CategoryId);//tree insert product
                string productGroupName = _categoryService.GetCategoryByCategoryId(model.ProductGroupId).CategoryName;
               
                var productGroupTreeForCategory = (ProductGroupTree)categoryTable[categoryTop.CategoryName];
                ProductGroupTreeNode node = productGroupTreeForCategory.GetProductTreeNodeByProductGroupName(productGroupTreeForCategory.GetRoot(), newProductGroupName);
                ProductItemModel productItem = new ProductItemModel();
                productItem.BrandName = model.BrandName;
                productItem.ModelName = model.ModelName;
                productItem.ProductCost = model.Cost;
                productItem.ProductDescription = model.ProductDescription;
                productItem.ProductPrice = model.Price;
                productItem.Status = true;
                productItem.ProductNumber = model.ProductNumber;
                productItem.ProductName = model.ProductName;
                node.ProductBase.Products.Add(productItem);

                #endregion
            }
            else
            {
                //ürün bilgileri değiştirilir

                PrepareCategoryHash categoryHash = new PrepareCategoryHash();
                categoryHash.CreateHashTableAndProductTree();
                Hashtable categoryTable = categoryHash.GetCategoryTable;
                var categories = _categoryService.GetAllCategory().Where(x => x.CategoryType == (byte)CategoryType.Category);
                List<ProductItemModel> productList = new List<ProductItemModel>();
                
                    var tree = (ProductGroupTree)categoryTable[categoryTop.CategoryName];

                    var treeNode = tree.GetProductTreeNodeByProductGroupName(tree.GetRoot(), categoryLast.CategoryName
                        );
                    if (treeNode.ProductBase != null)
                    {
                        var anyProduct = treeNode.ProductBase.Products.Where(x=>x.ProductId==model.ProductId).FirstOrDefault();
                        if (anyProduct != null)
                        {
                            anyProduct.ModelName = model.ModelName;
                            anyProduct.ProductCost = model.Cost;
                            anyProduct.ProductDescription = model.ProductDescription;
                            anyProduct.ProductName = model.ProductName;
                            anyProduct.ProductNumber = model.ProductNumber;
                            anyProduct.ProductPrice = model.Price;
                            anyProduct.BrandName = model.BrandName;
                          

                        }
                    }

                
            }
            #region databaseEdit
            if(model.CategoryId!=hdnCategoryId)
            {
                product.CategoryId = model.ProductGroupId;
            }
            product.BrandName = model.BrandName;
            product.ModelName = model.ModelName;
            product.ProductCost = model.Cost;
            product.ProductDescription = model.ProductDescription;
            product.ProductName = model.ProductName;
            product.ProductNumber = model.ProductNumber;
            product.ProductPrice = model.Price;
            _productservice.UpdateProduct(product);
            #endregion

            return RedirectToAction("Edit", new {id=model.ProductId,message="success" });
       }
        public ActionResult CalculateProfit()
        {
            decimal totalCost = 0;
            decimal totalPrice = 0;
            var invoices = _checkoutService.GetAllInvoice().Where(x=>x.Approval);
            foreach (var item in invoices)
            {
                var product = _productservice.GetProductByProductId(item.ProductId);
                totalCost = totalCost + product.ProductCost;
                totalPrice = totalPrice + product.ProductPrice;
                
            }
          decimal a = totalPrice - totalCost;
          ViewBag.TotalProfit = a.ToString("C");
            return View();
        }
       

    }
}