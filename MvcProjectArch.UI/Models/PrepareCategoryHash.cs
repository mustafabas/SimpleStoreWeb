using MvcProjectArch.Core.Infrastructure;
using MvcProjectArch.Services.Catolog;
using MvcProjectArch.UI.Models.Catolog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class PrepareCategoryHash
    {
        ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
        public Hashtable GetCategoryTable { get; set; }
       public PrepareCategoryHash()
        {
            this.GetCategoryTable = new Hashtable();
        }
        public void CreateHashTableAndProductTree()
        {

            var categories = categoryService.GetAllCategory().Where(x=>x.CategoryType==(byte)CategoryType.Category);
            foreach (var item in categories)
            {
                var productGroups = categoryService.GetCategoriesByCategoryParentId(item.ID);
                ProductGroupTree productTree=new ProductGroupTree();
                foreach (var productGroupItem in productGroups)
                
                {
                    ProductGroup productGroup = new ProductGroup();
                    var products = productGroupItem.CategoryProducts;

                    foreach (var productItem in products)
                    {
               
                        productGroup.Products.Add(new ProductItemModel {
                            ProductId = productItem.ID,
                            ProductName=productItem.ProductName,
                            ModelId=productItem.ModelId,
                            BrandId=productItem.BrandId,
                            CategoryId=productItem.CategoryId,
                            ProductCost=productItem.ProductCost,
                            ProductPrice=productItem.ProductPrice,
                            ProductNumber=productItem.ProductNumber,
                            Status=productItem.Status,
                            BrandName=productItem.BrandName,
                            ModelName=productItem.ModelName,
                            ProductImagePath=productItem.ProductImagePath,
                            ProductDescription=productItem.ProductDescription
                        });
                    }
                    productGroup.CategoryId = productGroupItem.ID;
                    productGroup.ProductGroupName = productGroupItem.CategoryName;
                    productTree.insert(productGroup);
                }
                this.GetCategoryTable.Add(item.CategoryName, productTree);
                
            }
            

        }
    }
}