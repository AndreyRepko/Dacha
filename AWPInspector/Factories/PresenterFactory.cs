using System;
using Dacha.Inspector.Dictionaries;

namespace Dacha.Inspector.Factories
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
            if (!addWindow.DialogResult.HasValue)
                throw new Exception("Dialog was closed without result");

            return addWindow.DialogResult.Value;
        }
    }
}
