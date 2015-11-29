using WPF.Dictionaries.Dictionaries;

namespace WPF.Dictionaries.Factories
{
    public interface IPresenterFactory
    {
        void PresentDictionary<T>(DictionaryViewModel<T>  dictionaryViewModel) where T : new();
        bool PresentDicionaryAdd<T>(DictionaryAddViewModel<T> addViewModel) where T : new();
    }
}
