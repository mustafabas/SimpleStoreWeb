using MvcProjectArch.UI.Models.Catolog;
using MvcProjectArh.Entities.Tables.Catolog;
using MvcProjectArh.Entities.Tables.Checkouts;
using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class AprioriHelper
    {
        public List<Customer> CustomerList { get; set; }
        public List<Product> ProductList { get; set; }
        public List<Invoice> InvoiceList { get; set; }
        private List<CustomerProductItem> CustomerProducst { get; set; }
        private List<ProductBinarySelectionItem> ProductBinaryList = new List<ProductBinarySelectionItem>();
        public List<int> ProductSuggestIds { get; set; }
        public int CheckoutProductSize { get; set; }


        public AprioriHelper(List<Customer> customerList, List<Product> productList, List<Invoice> invoiceList,int checkoutProductSize)
        {
            this.CustomerList = customerList;
            this.ProductList = productList;
            this.InvoiceList = invoiceList;
            this.CheckoutProductSize = checkoutProductSize;
            this.CustomerProducst = new List<CustomerProductItem>();
            this.ProductSuggestIds = new List<int>();
        }
        public void CreateProductSugges(List<Invoice> CheckotsProducts)
        {
            this.CreateCustomerProducts();
            this.CreateProductBinary();
            List<int> productsCustomer = new List<int>();
            foreach (var item in CheckotsProducts)
            {
                productsCustomer.Add(item.ProductId);
            }
            foreach (var item in CustomerProducst)
            {
                string productIds = "";
                List<int> productCostomerId=new List<int>();
                foreach (var item2 in productsCustomer)
                {
                   
                    productIds = productIds + "," + item2;
                    
                }
                if(productIds!="")
                {
                    productIds = productIds.Substring(1, productIds.Length - 1);
                    if (item.ProductIdsString.Contains(productIds))
                    {
                        
                        foreach (var item1 in productsCustomer)
                        {
                            item.ProductIds.Remove(item1);

                        }

                        ProductSuggestIds.AddRange(item.ProductIds);
                    }
                }
        
            }
        }
        public void CreateCustomerProducts()
        {
            foreach (var item in CustomerList)
            {
                var ProductIds = InvoiceList.Where(x=>x.Approval==true && x.CustomerID==item.ID).Select(x=>x.ID).Distinct().ToList();
                if(ProductIds.Count>=0)
                {
                    CustomerProductItem item1 = new CustomerProductItem();
                    item1.CustomerID = item.ID;
                    item1.ProductIds = ProductIds;
                    foreach (var item2 in ProductIds)
                    {
                        item1.ProductIdsString = item1.ProductIdsString + "," + item2;
                        
                    }
                    if(item1.ProductIdsString!=null)
                    {
                        item1.ProductIdsString = item1.ProductIdsString.Substring(1, item1.ProductIdsString.Length - 1);
                        CustomerProducst.Add(item1);
                    }
                 
                }
            }
        }
        public void CreateProductBinary()
        {
            ProductBinarySelectionItem item2=new ProductBinarySelectionItem();
            foreach (var item in CustomerProducst)
            {
                if (item.ProductIds.Count >= CheckoutProductSize)
                {
                    for (int i = 0; i < item.ProductIds.Count; i++)
                    {

                        for (int j = i + 1; j < item.ProductIds.Count; j++)
                        {
                            item2.ProductIds.Add(item.ProductIds[i]);
                            item2.ProductIds.Add(item.ProductIds[j]);
                            if (CheckoutProductSize > 2)
                            {
                                for (int a = j + 1; a < item.ProductIds.Count; a++)
                                {
                                    item2.ProductIds.Add(item.ProductIds[a]);
                                    if (CheckoutProductSize > 3)
                                    {
                                        for (int d = a+1; d < item.ProductIds.Count; d++)
                                        {
                                            item2.ProductIds.Add(item.ProductIds[d]);

                                        }
                                    }
                                }

                            }
                            item2.ProductIdsString = item2.ProductIds[0].ToString() + item2.ProductIds[1].ToString();

                            if (ProductBinaryList.Where(x => x.ProductIdsString == item2.ProductIdsString).FirstOrDefault() != null)
                            {
                                item2.Count++;
                            }
                            else
                            {

                                ProductBinaryList.Add(item2);
                            }

                        }
                    }
                }
                
            }
        }
       
    }
    public class SuggestItem
    {
        public int ProductID { get; set; }
        public int Degree { get; set; }
    }
    public class CustomerProductItem{
        public int CustomerID{get;set;}
        public string ProductIdsString { get; set; }
        public List<int> ProductIds{get;set;}
        public CustomerProductItem()
        {
            this.ProductIds=new List<int>();
        }
    }
    public class ProductBinarySelectionItem
    {

        public ProductBinarySelectionItem()
        {
            this.ProductIds = new List<int>();
        }
        public List<int> ProductIds;
        public string ProductIdsString { get; set; }
        public int Count { get; set; }
    }
}