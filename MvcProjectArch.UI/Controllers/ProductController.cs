using MvcProjectArch.Services.Catolog;
using MvcProjectArch.Services.Checkouts;
using MvcProjectArch.Services.Users;
using MvcProjectArch.UI.AuthHelper;
using MvcProjectArch.UI.Models;
using MvcProjectArch.UI.Models.Catolog;
using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        ICheckoutService _checkoutService;
        IProductService _productService;
        IUserService _customerService;
        public ProductController(ICheckoutService checkoutService, IProductService productService, IUserService customerService)
        {
            this._checkoutService = checkoutService;
            this._productService = productService;
            this._customerService = customerService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult nAdetUrunAdd()
        {

            return View();
        }
        [HttpGet]
        public ActionResult nAdetUrunGet(string categoryName,int count)
        {
            NAdetUrunGetModel model = new NAdetUrunGetModel();
            PrepareCategoryHash categoryHash = new PrepareCategoryHash();
            model.CategoryName = categoryName;
            model.Coun = count;
            categoryHash.CreateHashTableAndProductTree();
            var ProductTree=(ProductGroupTree)categoryHash.GetCategoryTable[categoryName];
            if (ProductTree != null)
            {
               //var productList = ProductTree.ProductHeap.GetProductByCount(count,ProductTree.TreeSize);
                ProductHeap heap = new ProductHeap();
                ProductTree.inOrderForProductsAllProduct(ProductTree.GetRoot());
                var productList = ProductTree.AllProductByCategoryName;
                foreach (var item in productList)
                {
                    heap.insert(item);
                }
                var productsLowPrice = heap.GetProductByCount(count,ProductTree.ProductTotalSize);//get lower product by count
                ProductTree.DeleteProducts(ProductTree.GetRoot(), productsLowPrice.Select(x => x.ProductId).ToList());//update products in tree
                model.Products = productsLowPrice;
                foreach (var item in productsLowPrice)
                {
                    Invoice invoice = new Invoice();
                    invoice.ProductId = item.ProductId;
                    invoice.CustomerID = CurrentUser.CustmerID;
                    invoice.DateTime = DateTime.Now;
                    invoice.Approval = false;
                    invoice.ProductNumber = 1;
                    _checkoutService.AddInvoice(invoice);//sepete eklendi
                    var product = _productService.GetProductByProductId(item.ProductId);//entitiy update
                    product.ProductNumber = product.ProductNumber - 1;
                    _productService.UpdateProduct(product);
                }
            }
            return View(model);
        }

        public ActionResult Checkouts(string message)
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
             if(message!=null)
             {
                 if(message=="success")
                 {
                     ViewBag.message = "Siparişiniz alınmıştır";
                 }
                 else if(message=="deleteSucc")
                 {
                     ViewBag.message = "Ürün Sepetten silinmiştir";
                 }
                 else
                 {
                     ViewBag.message = "Ürün Sepete Eklendi";

                 }

              
             }
            var invoiceProducts = _checkoutService.GetAllInvoice().Where(x=>x.Approval==false && x.CustomerID==CurrentUser.CustmerID);
            foreach (var item in invoiceProducts)
            {
                var product = _productService.GetProductByProductId(item.ProductId);
                model.Add(new InvoiceModel {DateTime=item.DateTime,Product=product,ProductNumber=item.ProductNumber,InvoiceId=item.ID });
                
            }
            var customerList=_customerService.GetAllCustomer().ToList();
            var invoice=_checkoutService.GetAllInvoice().Where(x=>x.Approval==true).ToList();
            var productList=_productService.GetAllProduct().ToList();
           int size= invoiceProducts.ToList().Count;
           //AprioriHelper helperSuggest = new AprioriHelper(customerList, productList, invoice, size);

           // helperSuggest.CreateProductSugges(invoiceProducts.ToList());
          
           // var gel = helperSuggest.ProductSuggestIds;
            
            return View(model);
        }
        [HttpPost]
         public ActionResult Checkouts()
         {
             var invoiceProducts = _checkoutService.GetAllInvoice().Where(x => x.Approval == false && x.CustomerID == CurrentUser.CustmerID);
             foreach (var item in invoiceProducts.ToList())
             {
                 item.Approval = true;

                 _checkoutService.UpdateInvoice(item);
             }

             return RedirectToAction("Checkouts", new { message="success"});
         }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult GetProductByKey(string key)
        {
            PrepareProductDescriptionTable descTable = new PrepareProductDescriptionTable();
            var products = _productService.GetAllProduct();
            List<ProductItemModel> productList = new List<ProductItemModel>();
            foreach (var item in products)
            {
                productList.Add(new ProductItemModel { ProductId = item.ID,ProductName=item.ProductName,ProductDescription=item.ProductDescription,
ProductPrice=item.ProductPrice,ModelName=item.ModelName,BrandName=item.BrandName });
            }
            descTable.CreateProductDescrptionTable(productList);
            List<ProductItemModel> ProductLis = (List<ProductItemModel>)descTable.ProductDescriptionTable[key];
            ViewBag.key = key;
            return View(ProductLis);
        }
        public ActionResult CheckoutAdd(int id)
        {
            var product = _productService.GetProductByProductId(id);
            product.ProductNumber = product.ProductNumber - 1;
            _productService.UpdateProduct(product);
            Invoice invoice = new Invoice();
            invoice.ProductId = id;
            invoice.ProductNumber = 1;
            invoice.CustomerID = CurrentUser.CustmerID;
            invoice.DateTime = DateTime.Now;
            invoice.Approval = false;
            _checkoutService.AddInvoice(invoice);
            return RedirectToAction("Checkouts", new { message = "sepetekle"});
        }
        public ActionResult CheckoutsDelete(int id)
        {
            var invoice = _checkoutService.GetAllInvoice().First(x => x.ID == id);
            var product = _productService.GetProductByProductId(invoice.ProductId);
            product.ProductNumber = product.ProductNumber + 1;
            _productService.UpdateProduct(product);
            _checkoutService.DeleteInvoice(invoice);

            return RedirectToAction("Checkouts", new { message = "deleteSucc" });
        }
       
        
    }
}