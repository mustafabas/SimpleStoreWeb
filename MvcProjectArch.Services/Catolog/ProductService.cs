using MvcProjectArch.Core.Data;
using MvcProjectArh.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Catolog
{
    public class ProductService:IProductService
    {
        IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;

        }
        public IList<Product> GetProductsByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException();
            var query = _productRepository.Table;
            query = query.Where(x=>x.CategoryId==categoryId);
            return query.ToList();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();
           
                _productRepository.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            _productRepository.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            _productRepository.Delete(product);
        }


        public Product GetProductByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException();
            var query = _productRepository.Table;
            return query.FirstOrDefault(x=>x.ID==productId);
        }


        public List<Product> GetAllProduct()
        {
            var query = _productRepository.Table;
            return query.ToList();
        }
    }
}
