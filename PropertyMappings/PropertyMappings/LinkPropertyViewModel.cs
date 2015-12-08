using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataStructures.DataStructures;
using WPFUtils;

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
