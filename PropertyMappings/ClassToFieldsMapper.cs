using System;
using System.Collections.ObjectModel;
using Dacha.PropertyMappings.PropertyMappings;

namespace Dacha.PropertyMappings
{
    public static class ClassToFieldsMapper
    {
        public static ObservableCollection<IPropertyViewModel> GetFieldsFromClass<T>(T value)
        {
            var result = new ObservableCollection<IPropertyViewModel>();
            return result;
        } 
    }
}
