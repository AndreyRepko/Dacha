using BasicDataStructures.Interfaces;
using WPF.Dictionaries.Factories;

namespace WPF.Dictionaries.CustomForms
{
    public interface ICustomFormViewModel
    {
        IPresenterFactory Presenter { get; set; }

        IWorkerServices DataServices { get; set; }

        bool? DialogResult { get; set; }
    }
}
