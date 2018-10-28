using MvcProjectArch.Core.Data;
using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Checkouts
{
    public class CheckoutService:ICheckoutService
    {
        IRepository<Invoice> _invoiceRepository;
        public CheckoutService(IRepository<Invoice> invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }


        public List<Invoice> GetAllInvoice()
        {
            var query = _invoiceRepository.Table;
            return query.ToList();
        }

        public void AddInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException();
            _invoiceRepository.Insert(invoice);
        }

        public void DeleteInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException();
            _invoiceRepository.Delete(invoice);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException();
            _invoiceRepository.Update(invoice);
        }

    }
}
