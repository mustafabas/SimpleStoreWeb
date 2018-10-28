using MvcProjectArh.Entities;
using MvcProjectArh.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Entities.Tables.Catolog
{
    public class Category:BaseEntity
    {
        private ICollection<Product> _categoryProducts;
        private ICollection<Product> _brandProducts;
        private ICollection<Product> _modelProducts;
        public int ID { get; set; }
        public int CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public byte CategoryType { get; set; }
        public bool Status { get; set; }



        public virtual ICollection<Product> CategoryProducts
        {
            get { return _categoryProducts ?? (_categoryProducts = new List<Product>()); }
            protected set { _categoryProducts = value; }
        }
        public virtual ICollection<Product> BrandProducts
        {
            get{ return _brandProducts??(_brandProducts=new List<Product>());}
            protected set { _brandProducts = value; }
        }
        public virtual ICollection<Product> ModelProducts
        {
            get { return _modelProducts ?? (_modelProducts = new List<Product>()); }
            protected set { _modelProducts = value; }
        }

    }
}
