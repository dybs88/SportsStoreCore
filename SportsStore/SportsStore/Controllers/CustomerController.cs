using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;

namespace SportsStore.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        private IAddressRepository _addressRepository;
        private UserManager<SportUser> _userManager;

        public CustomerController(ICustomerRepository custRepo, IAddressRepository addressRepo, UserManager<SportUser> userManager)
        {
            _customerRepository = custRepo;
            _addressRepository = addressRepo;
            _userManager = userManager;
        }

        public IActionResult Index(int customerId)
        {
            ViewBag.CustomerId = customerId;
            Customer customer = _customerRepository.GetCustomer(customerId);
            return View(customer);
        }

        public IActionResult Edit(int customerId)
        {
            ViewBag.CustomerId = customerId;
            Customer customer = _customerRepository.GetCustomer(customerId);
            return View(customer);
        }

        [HttpPost]
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

        public IActionResult Addresses(int customerId)
        {
            ViewBag.CustomerId = customerId;
            var customerAddresses = _addressRepository.GetCustomerAddresses(customerId);
            return View(customerAddresses.ToList());
        }

        public IActionResult CreateAddress(int customerId)
        {
            CreateAddressViewModel model = new CreateAddressViewModel { CustomerId = customerId, Address = new Address() };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAddress(CreateAddressViewModel model)
        {
            if(ModelState.IsValid)
            {
                _addressRepository.SaveAddress(model.Address);
                return RedirectToAction("Addresses", "Customer", new { customerId = model.CustomerId });
            }

            return View(model);
        }

        public IActionResult EditAddress(int addressId)
        {
            Address address = null;
            if (addressId == 0)
                address = new Address();
            else
                address = _addressRepository.GetAddress(addressId);
            return View(address);
        }

        [HttpPost]
        public IActionResult EditAddress(Address address)
        {
            if(ModelState.IsValid)
            {
                _addressRepository.SaveAddress(address);
                return RedirectToAction("Addresses", "Customer", new { address.CustomerId });
            }

            return View(address);
        }

        public IActionResult List()
        {
            return View(_customerRepository.Customers.ToList());
        }
    }
}