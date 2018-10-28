using MvcProjectArh.Entities.Tables.Catolog;
using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArh.Entities.Tables.Checkouts
{
    public class Invoice:BaseEntity
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductId { get; set; }
        public int ProductNumber { get; set; }
        public DateTime DateTime { get; set; }
        public bool Approval { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
