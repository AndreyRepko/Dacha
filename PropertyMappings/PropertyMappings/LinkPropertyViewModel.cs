using System;
using System.Collections.Generic;
using System.Linq;
using BasicDataStructures.DataStructures;

namespace Dacha.PropertyMappings.PropertyMappings
{
    public class LinkPropertyViewModel : BasicPropertyViewModel<long?>
    {
        private ItemPresenterViewModel _selectedItem;
        private List<ItemPresenterViewModel> _items;

        public LinkPropertyViewModel(Func<List<ItemPresenterViewModel>> itemsGetter)
        {
            ItemsGetter = itemsGetter;
        }

        public List<ItemPresenterViewModel> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                return _items = ItemsGetter();
            }
        }

        protected override void SetValue(long? value)
        {
            base.SetValue(value);
            SelectedItem = Items.FirstOrDefault(item => item.Id == value);
        }

        public Func<List<ItemPresenterViewModel>> ItemsGetter { get; private set; }

        public ItemPresenterViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    if (value != null)
                        Value = value.Id;
                    else
                        Value = null;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
    }
}
