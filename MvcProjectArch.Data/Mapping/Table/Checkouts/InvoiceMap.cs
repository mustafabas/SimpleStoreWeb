using MvcProjectArh.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Data.Mapping.Table.Checkouts
{
    public class InvoiceMap:EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            this.ToTable("Invoice");
            this.Ignore(x=>x.Id);
            this.HasKey(x=>x.ID);
            this.HasRequired(x => x.Customer).WithMany(x => x.Invoices).HasForeignKey(x => x.CustomerID);
            this.HasRequired(x => x.Product).WithMany(x => x.Invoices).HasForeignKey(x => x.ProductId);
        }

    }
}
