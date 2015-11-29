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

        public bool PresentDicionaryAdd<T>(DictionaryAddEditViewModel<T> addEditViewModel) where T : new()
        {
            var addWindow = new DictionaryAddWindow {DataContext = addEditViewModel};
            addWindow.ShowDialog();
            if (!addEditViewModel.DialogResult.HasValue)
                throw new Exception("Dialog was closed without result");

            return addEditViewModel.DialogResult.Value;
        }

        public bool PresentRemove()
        {
            //TODO: Ask user before deletion!
            return true;
        }
    }
}
