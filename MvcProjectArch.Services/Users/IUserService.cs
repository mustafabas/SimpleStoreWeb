using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Users
{
    public interface IUserService
    {
        User GetUserByUserID(int userId);
        List<User> GetAllUser();
        List<Customer> GetAllCustomer();
        void AddCustomer(Customer customer);
        
    }
}
