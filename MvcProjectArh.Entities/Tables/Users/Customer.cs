using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArh.Entities.Tables.Users
{
    public class Customer:BaseEntity
    {
        public int ID { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Gender { get; set; }
        public string Age { get; set; }
        public decimal Salary { get; set; }
        public bool Condition { get; set; }
        public string Occupation { get; set; }
        public string City { get; set; }

        private ICollection<Invoice> _invoices;
        public virtual ICollection<Invoice> Invoices
        {
            get { return _invoices ?? (_invoices = new List<Invoice>()); }
            protected set { _invoices = value; }
        }

    }
}
