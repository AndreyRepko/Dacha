using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Dacha.PropertyMappings.PropertyMappings;

namespace Dacha.PropertyMappings
{
    public static class ClassToFieldsMapper
    {
        private static Dictionary<Type, Func<string,object,string, IPropertyViewModel>> _typeMaping;

        static ClassToFieldsMapper()
        {
            _typeMaping = new Dictionary<Type, Func<string, object, string, IPropertyViewModel>>();

            _typeMaping[typeof (string)] =
                (displayName, value, propertyName) => new StringPropertyViewModel {DisplayName = displayName, Value = (string) value, PropertyName = propertyName};
            _typeMaping[typeof (int)] =
                (displayName, value, propertyName) => new IntPropertyViewModel {DisplayName = displayName, Value = (int) value, PropertyName = propertyName };

        }

        public static TrulyObservableCollection<IPropertyViewModel> GetFieldsFromClass<T>(T value)
        {
            var result = new TrulyObservableCollection<IPropertyViewModel>();

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
                        ? mapper(attributes[0].DisplayName, prop.GetValue(value, null), prop.Name)
                        : mapper(attributes[0].DisplayName, null, prop.Name));
                }
                else
                {
                    throw new Exception($"Invalid attribute count {attributes.Length}");
                }
            }

            return result;
        }

        public static void FieldsUpdater<T>(ref T value, object sender, NotifyCollectionChangedEventArgs e) 
        {
            if (e.Action != NotifyCollectionChangedAction.Replace)
                throw new Exception($"Invalid action: {e.Action}");

            var type = typeof(T);
            var properties = type.GetProperties().Where(prop => prop.IsDefined(typeof(PropertyMapping), false)).ToList();
            if (e.NewItems.Count != 1 )
                throw new Exception($"Invalid replace! Not supported now : {e.NewItems.Count}");

            var item = e.NewItems[0] as IPropertyViewModel;

            if (item == null)
                throw new Exception($"Item is not interfaced by is: {e.NewItems[0].GetType()}");

            var theProperty = properties.First(p => p.Name == item.PropertyName);

            theProperty.SetValue(value, item.UntypedValue);
        }
    }
}
