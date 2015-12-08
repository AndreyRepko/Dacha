using System.ComponentModel;

namespace Dacha.PropertyMappings.PropertyMappings
{
    public class BasicPropertyViewModel<T> : IPropertyViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string DisplayName { get; set; }
        private T _value;

        protected virtual void SetValue(T value)
        {
            if (!Equals(_value, value))
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public T Value
        {
            get { return _value; }
            set { SetValue(value); }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object UntypedValue => _value;
        public string PropertyName { get; set; }
    }
}
