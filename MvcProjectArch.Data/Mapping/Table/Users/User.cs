using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Data.Mapping.Table.Users
{
   public  class UserMap:EntityTypeConfiguration<User>
    {
       public UserMap()
       {
           this.ToTable("User");
           this.Ignore(u=>u.Id);
           this.HasKey(u => u.ID);

       }
    }
}
