using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Infrastructure.Abstract;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Base;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;

namespace SportsStore.Controllers
{
    [Authorize]
    public class CustomerController : BaseController, ISearchable
    {
        private ICustomerRepository _customerRepository;
        private IAddressRepository _addressRepository;
        private IOrderRepository _orderRepository;

        public CustomerController(IServiceProvider provider, IConfiguration config, ICustomerRepository custRepo, IAddressRepository addressRepo, IOrderRepository orderRepo)
            :base(provider, config)
        {
            _customerRepository = custRepo;
            _addressRepository = addressRepo;
            _orderRepository = orderRepo;
        }

        [Authorize(Policy = CustomerPermissionValues.ViewCustomer)]
        public async Task<IActionResult> List(int currentPage = 1)
        {
            CustomerListViewModel model = new CustomerListViewModel(currentPage, _pageSize, _customerRepository.Customers.Count());

            var customers = PaginateList(_customerRepository.Customers, currentPage);

            foreach(var customer in customers)
            {
                model.Customers.Add(await _customerRepository.GetCustomerFullData(customer));
            }
            return View(model);
        }

        [Authorize(Policy = CustomerPermissionValues.ViewCustomer)]
        [HttpPost]
        public async Task<IActionResult> List(BaseListViewModel model)
        {
            if(model == null || string.IsNullOrEmpty(model.SearchData))
            {
                return await List();
            }
            else
            {
                List<Customer> customers = new List<Customer>();

                if (model.SearchData.Contains("@"))
                    customers.Add(_customerRepository.GetCustomer(model.SearchData));
                else
                    customers.Add(_customerRepository.GetCustomer(int.Parse(model.SearchData)));

                CustomerListViewModel customerModel = new CustomerListViewModel(1, _pageSize, customers.Count);

                foreach (var customer in customers)
                {
                    customerModel.Customers.Add(await _customerRepository.GetCustomerFullData(customer));
                }

                return View(customerModel);
            }
        }

        [Authorize(Policy = CustomerPermissionValues.ViewCustomer)]
        public IActionResult Index(int customerId)
        {
            ViewBag.CustomerId = customerId;
            Customer customer = _customerRepository.GetCustomer(customerId);
            return View(customer);
        }
        
        [Authorize(Policy = CustomerPermissionValues.EditCustomer)]
        public IActionResult Edit(int customerId)
        {
            ViewBag.CustomerId = customerId;
            Customer customer = _customerRepository.GetCustomer(customerId);
            return View(customer);
        }

        [HttpPost]
        [Authorize(Policy = CustomerPermissionValues.EditCustomer)]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.SaveCustomer(customer);
                return RedirectToAction("Index", "Customer", new { customerId = customer.CustomerId });
            }
            else
                return View(customer);
        }

        [Authorize(Policy = CustomerPermissionValues.ViewCustomer)]
        [Authorize(Policy = CustomerPermissionValues.ViewAddress)]
        public IActionResult Addresses(int customerId)
        {
            ViewBag.CustomerId = customerId;
            var customerAddresses = _addressRepository.GetCustomerAddresses(customerId);
            return View(customerAddresses.ToList());
        }

        [Authorize(Policy = CustomerPermissionValues.AddAddress)]
        public IActionResult CreateAddress(int customerId)
        {
            ViewBag.CustomerId = customerId;
            CreateAddressViewModel model = new CreateAddressViewModel { CustomerId = customerId, Address = new Address() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = CustomerPermissionValues.AddAddress)]
        public IActionResult CreateAddress(CreateAddressViewModel model)
        {
            if(ModelState.IsValid)
            {
                _addressRepository.SaveAddress(model.Address);
                return RedirectToAction("Addresses", "Customer", new { customerId = model.CustomerId });
            }

            return View(model);
        }

        [Authorize(Policy = CustomerPermissionValues.EditAddress)]
        public IActionResult EditAddress(int customerId, int addressId)
        {
            ViewBag.CustomerId = customerId;
            Address address = null;
            if (addressId == 0)
                address = new Address();
            else
                address = _addressRepository.GetAddress(addressId);

            return View(address);
        }

        [HttpPost]
        [Authorize(Policy = CustomerPermissionValues.EditAddress)]
        public IActionResult EditAddress(Address address)
        {
            if(ModelState.IsValid)
            {
                _addressRepository.SaveAddress(address);
                return RedirectToAction("Addresses", "Customer", new { address.CustomerId });
            }

            return View(address);
        }

        [Authorize(Policy = CustomerPermissionValues.DeleteAddress)]
        public IActionResult DeleteAddress(int addressId, int customerId)
        {
            _addressRepository.DeleteAddress(addressId);
            return RedirectToAction("Addresses", "Customer", new { customerId = customerId });
        }
    }
}