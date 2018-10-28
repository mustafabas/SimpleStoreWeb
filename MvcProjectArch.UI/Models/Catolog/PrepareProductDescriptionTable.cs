using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class PrepareProductDescriptionTable
    {
        public Hashtable ProductDescriptionTable { get; set; }
        public PrepareProductDescriptionTable()
        {
            this.ProductDescriptionTable = new Hashtable();
        }

        public void CreateProductDescrptionTable(List<ProductItemModel> Products)
        {
            foreach (var item in Products)
            {
                if(item.ProductDescription!=null)
                {
                    string[] productDescKey = item.ProductDescription.Split(',');
                    List<ProductItemModel> listProduct;
                    foreach (var item2 in productDescKey)
                    {
                        var list = ((List<ProductItemModel>)ProductDescriptionTable[item2]);
                        if (list != null)
                        {
                            ((List<ProductItemModel>)ProductDescriptionTable[item2]).Add(item);

                        }
                        else
                        {
                            listProduct = new List<ProductItemModel>();
                            listProduct.Add(item);
                            ProductDescriptionTable.Add(item2, listProduct);
                        }



                    }
                }
            }
        }
    }
}