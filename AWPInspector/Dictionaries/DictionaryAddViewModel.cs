using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using Dacha.Inspector.Annotations;
using Dacha.PropertyMappings;
using Dacha.PropertyMappings.PropertyMappings;
using Dacha.WPFUtils;

namespace Dacha.Inspector.Dictionaries
{
    public class DictionaryAddViewModel<T>: INotifyPropertyChanged where T : new()
    {
        private T _value;
        private bool? _dialogResult;
        private RelayCommand _okayCommand;

        public T Value
        {
            get { return _value; }
        }

        public DictionaryAddViewModel()
        {
            _value = new T();
            DictionaryContent = new DisplayDictionaryViewModel();
            DictionaryContent.Fields = ClassToFieldsMapper.GetFieldsFromClass(Value);
            DictionaryContent.Fields.CollectionChanged += (sender, e)=>ClassToFieldsMapper.FieldsUpdater(ref _value, sender, e);
            DialogResult = false;
        } 

        public DisplayDictionaryViewModel DictionaryContent { get; set; }

        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (value != _dialogResult)
                {
                    _dialogResult = value;
                    OnPropertyChanged(nameof(DialogResult));
                }
            }
        }

        public RelayCommand OkayCommand => _okayCommand ?? (_okayCommand = new RelayCommand(() => DialogResult = true));

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryAddViewModelDummy : DictionaryAddViewModel<int> {
    }
}
