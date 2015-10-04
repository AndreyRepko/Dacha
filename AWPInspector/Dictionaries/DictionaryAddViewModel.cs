using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Dacha.Inspector.Annotations;
using WPFUtils.PropertyMappings;

namespace Dacha.Inspector.Dictionaries
{
    public class DictionaryAddViewModel<T>: INotifyPropertyChanged where T : new()
    {
        private T _value;
        public DictionaryAddViewModel()
        {
            _value = new T();
            DictionaryContent = new DisplayDictionaryViewModel();
            DictionaryContent.Fields = new ObservableCollection<IPropertyViewModel> {new StringPropertyViewModel() {DisplayName = "blabla", Value = "scuko"} };
        } 

        public DisplayDictionaryViewModel DictionaryContent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryAddViewModelDummy : DictionaryAddViewModel<int> { }
}
