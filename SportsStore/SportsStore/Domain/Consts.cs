using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Domain
{
    public static class IdentityRoleNames
    {
        public const string Admins = "Administracja";
        public const string Clients = "Klient sklepu";
        public const string Warehouse = "Magazyn";
        public const string Sales = "Sprzedaż";
        public const string Service = "Biuro Obsługi Klienta";
        public const string Employees = "Pracownik sklepu";
    }

    public static class SecurityPermissionValues
    {
        public const string ViewUser = "Podgląd użytkownika";
        public const string AddUser = "Dodawanie użytkownika";
        public const string EditUser = "Edytowanie użytkownika";
        public const string DeleteUser = "Usuwanie użytkownika";

        public const string ViewRole = "Podgląd roli";
        public const string AddRole = "Dodawanie roli";
        public const string EditRole = "Edytowanie roli";
        public const string DeleteRole = "Usuwanie roli";
    }

    public static class CustomerPermissionValues
    {
        public const string ViewCustomer = "Podgląd klienta";
        public const string AddCustomer = "Dodawanie klienta sklepu";
        public const string EditCustomer = "Edytowanie klienta sklepu";
        public const string DeleteCustomer = "Usuwanie klienta sklepu";

        public const string ViewAddress = "Podgląd adresu";
        public const string AddAddress = "Dodawanie adresu";
        public const string EditAddress = "Edytowanie adresu";
        public const string DeleteAddress = "Usuwanie adresu";
    }

    public static class SalesPermissionValues
    {
        public const string ViewOrder = "Podgląd zamówienia";
        public const string AddOrder = "Dodawanie zamówienia";
        public const string EditOrder = "Edytowanie zamówienia";
        public const string DeleteOrder = "Usuwanie zamówienia";
    }

    public static class SecurityPermissionCategories
    {
        public const string Security = "Bezpieczeństwo";
        public const string Customer = "Klienci";
        public const string Sales = "Sprzedaż";
    }

    public static class SportsStoreClaimTypes
    {
        public const string CustomerId = "http://sportsstore/claims/customerId";
        public const string AddressId = "http://sportsstore/claims/addressId";
    }
}
