using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcProjectArh.Entities.Tables.Catolog;

namespace MvcProjectArch.Data.Mapping.Catolog
{
   public class ProductMap:EntityTypeConfiguration<Product>
    {
       public ProductMap()
       {
           this.ToTable("Product");
           this.Ignore(p => p.Id);
           this.HasKey(p => p.ID);

           this.HasRequired(x => x.Category).WithMany(x => x.CategoryProducts).HasForeignKey(x => x.CategoryId);
           this.HasRequired(p => p.Model).WithMany(m => m.ModelProducts).HasForeignKey(p=> p.ModelId);
           this.HasRequired(p => p.Brand).WithMany(b => b.BrandProducts).HasForeignKey(p=>p.BrandId);
         

       }

    }
}
