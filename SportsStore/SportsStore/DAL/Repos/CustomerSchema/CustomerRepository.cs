using Microsoft.AspNetCore.Identity;
using SportsStore.DAL.AbstractContexts;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos.Security;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.CustomerSchema
{
    public class CustomerRepository : ICustomerRepository
    {
        private IApplicationDbContext _context;
        private ISportsStoreUserManager _userManager;
        private IAddressRepository _addressRepository;
        private IOrderRepository _orderRepository;

        public IEnumerable<Customer> Customers => _context.Customers;

        public CustomerRepository(IApplicationDbContext context, ISportsStoreUserManager userManager, IAddressRepository addressRepo, IOrderRepository orderRepo)
        {
            _context = context;
            _userManager = userManager;
            _addressRepository = addressRepo;
            _orderRepository = orderRepo;
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
                _context.Customers.Remove(customer);

            _context.SaveChanges();
        }

        public Customer GetCustomer(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public CustomerAdditionalData GetCustomerAdditionalData(int customerId)
        {
            CustomerAdditionalData result = new CustomerAdditionalData(customerId);
            result.CustomerOrdersCount = _orderRepository.GetCustomerOrders(customerId).Count();

            return result;
        }

        public async Task<CustomerFullData> GetCustomerFullData(Customer customer)
        {
            if (customer != null)
            {
                return new CustomerFullData
                {
                    Customer = customer,
                    User = await _userManager.FindByEmailAsync(customer.Email),
                    Addresses = _addressRepository.GetCustomerAddresses(customer.CustomerId),
                    AdditionalData = GetCustomerAdditionalData(customer.CustomerId)
                };
            }

            return null;
        }

        public async Task<CustomerFullData> GetCustomerFullData(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
            {
                return new CustomerFullData
                {
                    Customer = customer,
                    User = await _userManager.FindByEmailAsync(customer.Email), 
                    Addresses = _addressRepository.GetCustomerAddresses(customerId),
                    AdditionalData = GetCustomerAdditionalData(customerId)
                };
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
