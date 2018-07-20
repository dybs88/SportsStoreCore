using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class ObservableCollection
    {
        public static ObservableCollection<TObject> AddRange<TObject>(this ObservableCollection<TObject> collection, IEnumerable<TObject> items)
        {
            foreach(var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
