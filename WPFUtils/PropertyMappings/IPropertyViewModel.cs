using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtils.PropertyMappings
{
    public interface IPropertyViewModel : INotifyPropertyChanged
    {
        string DisplayName { get; set; }

        object UntypedValue { get; }
    }
}
