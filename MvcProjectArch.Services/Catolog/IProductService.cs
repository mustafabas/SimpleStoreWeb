using MvcProjectArh.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Catolog
{
    public interface IProductService
    {
        IList<Product> GetProductsByCategoryId(int categoryId);
        List<Product> GetAllProduct();
        Product GetProductByProductId(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

    }
}
