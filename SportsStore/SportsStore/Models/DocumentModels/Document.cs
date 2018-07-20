using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SportsStore.Models.DocumentModels
{
    public abstract class Document : INotifyPropertyChanged
    {
        public int DocumentTypeId { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        { }
    }
}
