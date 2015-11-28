using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dacha.Inspector.Annotations;
using Dacha.Inspector.Factories;
using Dacha.WPFUtils;

namespace Dacha.Inspector.Dictionaries
{
    public class DictionaryViewModel<T> : INotifyPropertyChanged where T : new()
    {
        private List<T> _dictionary;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private readonly RelayCommand _removeCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public Func<List<T>> DictionaryGetter { get; set; }
        public Action<T> DictionaryAdder { get; set; }
        public Action<T> DictionaryDeleter { get; set; }


        public List<T> Dictionary => DictionaryGetter();

        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(Add));

        private void Add()
        {
            var addModel = new DictionaryAddViewModel<T>();
            if (Presenter.PresentDicionaryAdd(addModel))
            {
                DictionaryAdder(addModel.Value);
                OnPropertyChanged(nameof(Dictionary));
            }

        }

        public RelayCommand EditCommand
        {
            get { return _editCommand; }
        }

        public RelayCommand RemoveCommand
        {
            get { return _removeCommand; }
        }

        public IPresenterFactory Presenter { get; set; }

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
