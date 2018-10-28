using MvcProjectArch.Core.Data;
using MvcProjectArh.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectArch.Services.Users
{
    public class UserService:IUserService
    {
        IRepository<User> _userRepository;
        IRepository<Customer> _customerRepository;
        public UserService(IRepository<User> userRepository, IRepository<Customer> customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;

        }
        public User GetUserByUserID(int userId)
        {
            if (userId == null)
                throw new ArgumentNullException();
            var query = _userRepository.Table;
            return query.FirstOrDefault(x=>x.ID==userId);
        }

        public List<User> GetAllUser()
        {
            var query = _userRepository.Table;
            return query.ToList();
        }


        public List<Customer> GetAllCustomer()
        {
            var query = _customerRepository.Table;
            return query.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException();
            _customerRepository.Insert(customer);
        }
    }
}
