using Microsoft.AspNetCore.Identity;
using SportsStore.DAL.Contexts;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.CustomerSchema
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;
        private UserManager<SportUser> _userManager;

        public IEnumerable<Customer> Customers => _context.Customers;

        public CustomerRepository(ApplicationDbContext context, UserManager<SportUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public int SaveCustomer (Customer customer)
        {
            if (customer.CustomerId == 0)
                _context.Customers.Add(customer);
            else
            {
                var dbCustomer = Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                dbCustomer.FirstName = customer.FirstName;
                dbCustomer.LastName = customer.LastName;
                dbCustomer.Email = customer.Email;
                dbCustomer.PhoneNumber = customer.PhoneNumber;
                var user = _userManager.Users.First(u => u.CustomerId == customer.CustomerId);
                user.Email = customer.Email;
                _userManager.UpdateAsync(user);
            }


            _context.SaveChanges();

            return customer.CustomerId;
        }
    }
}
