using System.ComponentModel;
using BasicDataStructures.Interfaces;
using WPF.Dictionaries.Factories;

namespace WPF.Dictionaries.CustomForms
{
    public interface ICustomFormViewModel : INotifyPropertyChanged
    {
        IPresenterFactory Presenter { get; set; }

        IWorkerServices DataServices { get; set; }
    }
}
