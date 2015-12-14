using WPF.Dictionaries.CustomForms;
using WPF.Dictionaries.Dictionaries;

namespace WPF.Dictionaries.Factories
{
    public interface IPresenterFactory
    {
        void PresentDictionary<T>(DictionaryViewModel<T>  dictionaryViewModel) where T : new();
        bool PresentDicionaryAdd<T>(DictionaryAddEditViewModel<T> addEditViewModel) where T : new();
        bool PresentRemove();
        bool PresentCustomForm(ICustomFormViewModel viewModel);
    }
}
