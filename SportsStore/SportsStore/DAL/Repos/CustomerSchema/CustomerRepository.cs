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
        private IAddressRepository _addressRepository;

        public IEnumerable<Customer> Customers => _context.Customers;

        public CustomerRepository(ApplicationDbContext context, UserManager<SportUser> userManager, IAddressRepository addressRepo)
        {
            _context = context;
            _userManager = userManager;
            _addressRepository = addressRepo;
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
                _context.Customers.Remove(customer);

            _context.SaveChanges();
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public async Task<CustomerFullData> GetCustomerFullData(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if(customer != null)
            {
                var user = await _userManager.FindByEmailAsync(customer.Email);
                var addresses = _addressRepository.GetCustomerAddresses(customerId);

                return new CustomerFullData { Customer = customer, User = user, Addresses = addresses };
            }

            return null;
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
