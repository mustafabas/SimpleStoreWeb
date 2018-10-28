using MvcProjectArh.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public Product Product { get; set; }
        public int ProductNumber { get; set; }
        public DateTime DateTime { get; set; }
        
    }
    
}