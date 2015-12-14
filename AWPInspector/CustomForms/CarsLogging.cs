using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BasicDataStructures.Interfaces;
using WPF.Dictionaries.Annotations;
using WPF.Dictionaries.CustomForms;
using WPF.Dictionaries.Factories;

namespace Dacha.Inspector.CustomForms
{
    internal class CarsLogging : ICustomFormViewModel
    {
        public IPresenterFactory Presenter { get; set; }
        public IWorkerServices DataServices { get; set; }
        public bool? FormResult { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
