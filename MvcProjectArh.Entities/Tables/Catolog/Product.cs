using MvcProjectArch.Entities.Tables.Catolog;
using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArh.Entities.Tables.Catolog
{
    public class Product:BaseEntity
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImagePath { get; set; }
        public int ProductNumber { get; set; }
        public decimal ProductCost { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }

        private ICollection<Invoice> _invoices;
        public virtual ICollection<Invoice> Invoices
        {
            get { return _invoices ?? (_invoices = new List<Invoice>()); }
            protected set { _invoices = value; }
        }
        public virtual Category Category { get; set; }
        public virtual Category Brand { get; set; }
        public virtual Category Model { get; set; }
    }
}
