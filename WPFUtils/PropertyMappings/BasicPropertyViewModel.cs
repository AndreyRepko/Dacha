using System.ComponentModel;

namespace WPFUtils.PropertyMappings
{
    public class BasicPropertyViewModel<T> : IPropertyViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string DisplayName { get; set; }
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object UntypedValue => _value;
    }
}
