using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BasicDataStructures.Interfaces;
using Dacha.WPFUtils;
using WPF.Dictionaries.Factories;

namespace WPF.Dictionaries.Dictionaries
{
    public class DictionaryViewModel<T> : INotifyPropertyChanged where T : new()
    {
        private List<T> _dictionary;

        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public Func<List<T>> DictionaryGetter { get; set; }
        public Action<T> DictionarySaver { get; set; }
        public Action<T> DictionaryDeleter { get; set; }


        public List<T> Dictionary => DictionaryGetter();

        private T _selectedItem;

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if ((_selectedItem == null) && (value == null)) return;
                if ((_selectedItem != null) && _selectedItem.Equals(value)) return;

                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(Add));

        private void Add()
        {
            var addModel = new DictionaryAddEditViewModel<T>(DataServices);
            if (Presenter.PresentDicionaryAdd(addModel))
            {
                DictionarySaver(addModel.Value);
                OnPropertyChanged(nameof(Dictionary));
            }
        }

        public RelayCommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(Edit));

        private void Edit()
        {
            var addModel = new DictionaryAddEditViewModel<T>(SelectedItem, DataServices);
            if (Presenter.PresentDicionaryAdd(addModel))
            {
                DictionarySaver(addModel.Value);
                OnPropertyChanged(nameof(Dictionary));
            }
        }

        public RelayCommand RemoveCommand => _removeCommand ?? (_removeCommand = new RelayCommand(Remove));

        private void Remove()
        {
            if (Presenter.PresentRemove())
            {
                DictionaryDeleter(_selectedItem);
                OnPropertyChanged(nameof(Dictionary));
            }
        }

        public IPresenterFactory Presenter { get; set; }
        public IWorkerServices DataServices { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DictionaryViewModelDummy : DictionaryViewModel<int>
    {
    }
}
