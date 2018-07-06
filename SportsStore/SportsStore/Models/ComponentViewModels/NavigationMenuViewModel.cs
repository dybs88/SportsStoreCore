using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class NavigationMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public NavigationMenuViewModel(IEnumerable<string> categories, string selectedCategory)
        {
            Categories = categories;
            SelectedCategory = selectedCategory;
        }
    }
}
