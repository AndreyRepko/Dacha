using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dacha.Inspector.Annotations;
using Dacha.PropertyMappings.PropertyMappings;

namespace Dacha.Inspector.Dictionaries
{
    public class DisplayDictionaryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<IPropertyViewModel> _fields;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IPropertyViewModel> Fields
        {
            get { return _fields; }
            set
            {
                if (_fields != value)
                {
                    _fields = value;
                    OnPropertyChanged(nameof(Fields));
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
