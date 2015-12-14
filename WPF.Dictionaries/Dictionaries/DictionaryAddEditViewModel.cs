using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BasicDataStructures.Interfaces;
using Dacha.PropertyMappings;
using Dacha.WPFUtils;

namespace WPF.Dictionaries.Dictionaries
{
    public class DictionaryAddEditViewModel<T>: INotifyPropertyChanged where T : new()
    {
        private T _value;
        private bool? _dialogResult;
        private RelayCommand _okayCommand;
        private ClassToFieldsMapper _fieldsMapper;

        public T Value
        {
            get { return _value; }
        }

        public DictionaryAddEditViewModel(IWorkerServices dataServices)
        {
            _value = new T();
            _fieldsMapper = new ClassToFieldsMapper(dataServices);
            SetupContent();
        }

        public DictionaryAddEditViewModel(T editValue, IWorkerServices dataServices)
        {
            _value = editValue;
            _fieldsMapper = new ClassToFieldsMapper(dataServices);
            SetupContent();
        }

        private void SetupContent()
        {
            DictionaryContent = new DisplayDictionaryViewModel();
            DictionaryContent.Fields = _fieldsMapper.GetFieldsFromClass(Value);
            DictionaryContent.Fields.CollectionChanged += (sender, e) => _fieldsMapper.FieldsUpdater(ref _value, sender, e);
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
                    NotifyPropertyChanged(nameof(DialogResult));
                }
            }
        }

        public RelayCommand OkayCommand => _okayCommand ?? (_okayCommand = new RelayCommand(() => DialogResult = true));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryAddEditViewModelDummy : DictionaryAddEditViewModel<int> {
        public DictionaryAddEditViewModelDummy() : base(null)
        {
        }
    }
}
