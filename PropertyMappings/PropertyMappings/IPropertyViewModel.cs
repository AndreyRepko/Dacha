using System.ComponentModel;

namespace Dacha.PropertyMappings.PropertyMappings
{
    public interface IPropertyViewModel : INotifyPropertyChanged
    {
        string DisplayName { get; set; }

        object UntypedValue { get; }

        string PropertyName { get; }
    }
}
