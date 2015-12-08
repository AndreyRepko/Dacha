using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BasicDataStructures.DataStructures;
using BasicDataStructures.Interfaces;
using Dacha.PropertyMappings.PropertyMappings;
using WPFUtils;

namespace Dacha.PropertyMappings
{
    public class ClassToFieldsMapper
    {
        private readonly IWorkerServices _dataServices;
        private readonly Dictionary<Type, Func<string,object,string, IPropertyViewModel>> _simpleTypeMaping;

        public ClassToFieldsMapper(IWorkerServices dataServices)
        {
            _dataServices = dataServices;
            _simpleTypeMaping = new Dictionary<Type, Func<string, object, string, IPropertyViewModel>>();

            SetupSimpleTypes();
        }

        private void SetupSimpleTypes()
        {
            _simpleTypeMaping[typeof (string)] =
                (displayName, value, propertyName) =>
                    new StringPropertyViewModel {DisplayName = displayName, Value = (string) value, PropertyName = propertyName};
            _simpleTypeMaping[typeof (int)] =
                (displayName, value, propertyName) =>
                    new IntPropertyViewModel {DisplayName = displayName, Value = (int) value, PropertyName = propertyName};
            _simpleTypeMaping[typeof (double?)] =
                (displayName, value, propertyName) =>
                    new DoublePropertyViewModel {DisplayName = displayName, Value = (double?) value, PropertyName = propertyName};
            _simpleTypeMaping[typeof (double)] =
                (displayName, value, propertyName) =>
                    new DoublePropertyViewModel {DisplayName = displayName, Value = (double) value, PropertyName = propertyName};
        }

        public TrulyObservableCollection<IPropertyViewModel> GetFieldsFromClass<T>(T value)
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
                    if (_simpleTypeMaping.ContainsKey(propType))
                    {
                        var mapper = _simpleTypeMaping[propType];

                        result.Add(value != null
                            ? mapper(attributes[0].DisplayName, prop.GetValue(value, null), prop.Name)
                            : mapper(attributes[0].DisplayName, null, prop.Name));
                    }
                    else
                    {
                        var link = (PropertyLink[])prop.GetCustomAttributes(typeof(PropertyLink), false);
                        if (link.Length == 1)
                        {
                            Func<List<ItemPresenterViewModel>> itemsGetterResolved = 
                                () => _dataServices.GetItemsPresenterForEntity(link[0].ClassName);
                            result.Add(value != null
                            ? GetLinkViewModel(attributes[0].DisplayName, prop.GetValue(value, null), prop.Name,  itemsGetterResolved)
                            : GetLinkViewModel(attributes[0].DisplayName, null, prop.Name, itemsGetterResolved));
                        }
                        else
                        {
                            throw new Exception($"Invalid link attribute count {link.Length}");
                        }
                    }
                }
                else
                {
                    throw new Exception($"Invalid attribute count {attributes.Length}");
                }
            }

            return result;
        }

        private IPropertyViewModel GetLinkViewModel(string displayName, object value, string propertyName, 
            Func<List<ItemPresenterViewModel>> itemsGetter)
        {
            //Sorry guys, cruel hack. Dont want to use inheritance for all domain entitites. At least for now
            long? id = value==null ? null : ((dynamic) value).Id;
            return new LinkPropertyViewModel(itemsGetter)
            {
                DisplayName = displayName,
                Value = id,
                PropertyName = propertyName
            };
        }

        public void FieldsUpdater<T>(ref T value, object sender, NotifyCollectionChangedEventArgs e) 
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
            if (theProperty.IsDefined(typeof (PropertyLink), false))
            {
                var links = (PropertyLink[])(theProperty.GetCustomAttributes(typeof(PropertyLink), false));
                var link = links.FirstOrDefault();
                var linkItem = (LinkPropertyViewModel) item;
                theProperty.SetValue(value, _dataServices.GetItemByTypeAndId(link.ClassName, linkItem.Value));
            }
            else
                theProperty.SetValue(value, item.UntypedValue);
        }
    }
}
