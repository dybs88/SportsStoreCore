using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Domain;

namespace SportsStore.Models.ViewModels
{
    public class AdminNavigationMenuViewModel
    {

        public IEnumerable<AdminOption> AdminOptions => 
            new List<AdminOption>
            {
                new AdminOption{Name = "Profil", Action = "Index", Controller = "Admin"},
                new AdminOption{Name = "Produkty", Action = "Products", Controller = "Product"},
                new AdminOption{Name = "Zamówienia", Action = "List", Controller = "Order"},
                new AdminOption{Name = "Użytkownicy", Action = "List", Controller = "User"}
            };

        public AdminOption SelectedOption { get; set; }
    }
}
