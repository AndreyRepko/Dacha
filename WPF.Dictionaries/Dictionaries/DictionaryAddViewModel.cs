using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Dacha.PropertyMappings;
using Dacha.WPFUtils;

namespace WPF.Dictionaries.Dictionaries
{
    public class DictionaryAddEditViewModel<T>: INotifyPropertyChanged where T : new()
    {
        private T _value;
        private bool? _dialogResult;
        private RelayCommand _okayCommand;

        public T Value
        {
            get { return _value; }
        }

        public DictionaryAddEditViewModel()
        {
            _value = new T();
            SetupContent();
        }

        public DictionaryAddEditViewModel(T editValue)
        {
            _value = editValue;
            SetupContent();
        }

        private void SetupContent()
        {
            DictionaryContent = new DisplayDictionaryViewModel();
            DictionaryContent.Fields = ClassToFieldsMapper.GetFieldsFromClass(Value);
            DictionaryContent.Fields.CollectionChanged += (sender, e) => ClassToFieldsMapper.FieldsUpdater(ref _value, sender, e);
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryAddEditViewModelDummy : DictionaryAddEditViewModel<int> {
    }
}
