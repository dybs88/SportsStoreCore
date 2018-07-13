using SportsStore.DAL.Contexts;
using SportsStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.CustomerSchema
{
    public class AddressRepository : IAddressRepository
    {
        private ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Address> Addresses => _context.Addresses;

        public void DeleteAddress(int addressId)
        {
            var addressToRemove = Addresses.First(a => a.AddressId == addressId);
            _context.Addresses.Remove(addressToRemove);
            _context.SaveChanges();
        }

        public Address GetAddress(int addressId)
        {
            return Addresses.First(a => a.AddressId == addressId);
        }

        public IEnumerable<Address> GetCustomerAddresses(int customerId)
        {
            return Addresses.Where(a => a.CustomerId == customerId);
        }

        public int SaveAddress(Address address)
        {
            if (address.AddressId == 0)
                _context.Addresses.Add(address);
            else
            {
                var addressToUpdate = GetAddress(address.AddressId);

                if (addressToUpdate != null)
                {
                    addressToUpdate.City = address.City;
                    addressToUpdate.Street = address.Street;
                    addressToUpdate.Region = address.Region;
                    addressToUpdate.BuildingNumber = address.BuildingNumber;
                    addressToUpdate.ApartmentNumber = address.ApartmentNumber;
                    addressToUpdate.ZipCode = address.ZipCode;
                }
            }

            _context.SaveChanges();

            return address.AddressId;
        }
    }
}
