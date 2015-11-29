using System;
using WPF.Dictionaries.Dictionaries;

namespace WPF.Dictionaries.Factories
{
    public class PresenterFactory : IPresenterFactory
    {
        public void PresentDictionary<T>(DictionaryViewModel<T> dictionaryViewModel) where T : new()
        {
            var t = new DictionaryWindow {DataContext = dictionaryViewModel};
            t.ShowDialog();
        }

        public bool PresentDicionaryAdd<T>(DictionaryAddViewModel<T> addViewModel) where T : new()
        {
            var addWindow = new DictionaryAddWindow {DataContext = addViewModel};
            addWindow.ShowDialog();
            if (!addViewModel.DialogResult.HasValue)
                throw new Exception("Dialog was closed without result");

            return addViewModel.DialogResult.Value;
        }
    }
}
