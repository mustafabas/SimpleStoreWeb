using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Checkouts
{
    public interface ICheckoutService
    {
        List<Invoice> GetAllInvoice();
        void AddInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
    }
}
