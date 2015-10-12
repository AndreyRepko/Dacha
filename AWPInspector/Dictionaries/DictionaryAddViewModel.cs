using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Dacha.Inspector.Annotations;
using Dacha.PropertyMappings;
using Dacha.PropertyMappings.PropertyMappings;

namespace Dacha.Inspector.Dictionaries
{
    public class DictionaryAddViewModel<T>: INotifyPropertyChanged where T : new()
    {
        public T Value { get; }

        public DictionaryAddViewModel()
        {
            Value = new T();
            DictionaryContent = new DisplayDictionaryViewModel();
            DictionaryContent.Fields = ClassToFieldsMapper.GetFieldsFromClass(Value);
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
