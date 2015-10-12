using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Dacha.PropertyMappings.PropertyMappings;

namespace Dacha.PropertyMappings
{
    public static class ClassToFieldsMapper
    {
        private static Dictionary<Type, Func<string,object,IPropertyViewModel>> _typeMaping;

        static ClassToFieldsMapper()
        {
            _typeMaping = new Dictionary<Type, Func<string, object, IPropertyViewModel>>();

            _typeMaping[typeof (string)] =
                (displayName, value) => new StringPropertyViewModel {DisplayName = displayName, Value = (string) value};
            _typeMaping[typeof (int)] =
                (displayName, value) => new IntPropertyViewModel {DisplayName = displayName, Value = (int) value};

        }

        public static ObservableCollection<IPropertyViewModel> GetFieldsFromClass<T>(T value)
        {
            var result = new ObservableCollection<IPropertyViewModel>();

            var type = typeof (T);
            var properties = type.GetProperties().Where(prop => prop.IsDefined(typeof(PropertyMapping), false));
            foreach (var prop in properties)
            {
                var attributes = (PropertyMapping[])prop.GetCustomAttributes(typeof (PropertyMapping), false);
                if (attributes.Length == 1)
                {
                    var propType = prop.PropertyType;
                    if (!_typeMaping.ContainsKey(propType))
                        throw new Exception($"Type mapping missing for {propType}");
                    var mapper = _typeMaping[propType];

                    result.Add(value != null
                        ? mapper(attributes[0].DisplayName, prop.GetValue(value, null))
                        : mapper(attributes[0].DisplayName, null));
                }
                else
                {
                    throw new Exception($"Invalid attribute count {attributes.Length}");
                }
            }

            return result;
        } 
    }
}
