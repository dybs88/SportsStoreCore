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
    }

    public static class SecurityPermssionValues
    {
        public const string AddUser = "Dodawanie użytkownika";
        public const string EditUser = "Edytowanie użytkownika";
        public const string DeleteUser = "Usuwanie użytkownika";

        public const string AddRole = "Dodawanie roli";
        public const string EditRole = "Edytowanie roli";
        public const string DeleteRole = "Usuwanie roli";
    }

    public static class SecurityPermissionCategories
    {
        public const string Security = "Bezpieczeństwo";
    }
}
