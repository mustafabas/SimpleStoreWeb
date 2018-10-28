using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Data.Mapping.Table.Users
{
    public class CustomerMap:EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.Ignore(x => x.Id);
            this.HasKey(x=>x.ID);
        }
    }
}
