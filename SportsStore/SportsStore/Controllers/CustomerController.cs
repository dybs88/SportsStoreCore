using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;

namespace SportsStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        private IAddressRepository _addressRepository;
        private static UserManager<SportUser> _userManager;

        public CustomerController(ICustomerRepository custRepo, IAddressRepository addressRepo, UserManager<SportUser> userManager)
        {
            _customerRepository = custRepo;
            _addressRepository = addressRepo;
            _userManager = userManager;
        }

        [Authorize(Policy = CustomerPermissionValues.ViewCustomer)]
        public IActionResult List()
        {
            return View(_customerRepository.Customers.ToList());
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

            if (address.CustomerId != customerId)
                return Redirect("/Account/AccessDenied");

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