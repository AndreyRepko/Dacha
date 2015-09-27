using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dacha.Inspector.Annotations;
using Dacha.WPFUtils;

namespace Dacha.Inspector.Dictionaries
{
    public class DictionaryViewModel<T> : INotifyPropertyChanged
    {
        private List<T> _dictionary;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private readonly RelayCommand _removeCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<T> Dictionary
        {
            get { return _dictionary; }
            set
            {
                if (_dictionary != value)
                {
                    _dictionary = value;
                    OnPropertyChanged(nameof(Dictionary));
                }
            }
        }

        public RelayCommand AddCommand
        {
            get { return _addCommand; }
        }

        public RelayCommand EditCommand
        {
            get { return _editCommand; }
        }

        public RelayCommand RemoveCommand
        {
            get { return _removeCommand; }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryViewModelDummy : DictionaryViewModel<int>
    {
    }
}
