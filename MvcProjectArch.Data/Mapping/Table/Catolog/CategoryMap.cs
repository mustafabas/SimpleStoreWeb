using MvcProjectArch.Entities.Tables.Catolog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Data.Mapping.Table.Catolog
{
    public class CategoryMap:EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ID);
        }
    }
}
