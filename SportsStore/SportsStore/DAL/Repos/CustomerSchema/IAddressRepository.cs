using SportsStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.CustomerSchema
{
    public interface IAddressRepository
    {
        IEnumerable<Address> Addresses { get;  }
        bool CheckIfCustomerIsAddressOwner(int customerId, int addressId);
        void DeleteAddress(int addressId);
        Address GetAddress(int addressId);
        IEnumerable<Address> GetCustomerAddresses(int customerId);
        int SaveAddress(Address address);
    }
}
