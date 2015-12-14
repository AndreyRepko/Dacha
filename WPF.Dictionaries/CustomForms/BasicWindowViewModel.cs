using System.ComponentModel;
using System.Runtime.CompilerServices;
using BasicDataStructures.Interfaces;
using Dacha.WPFUtils;
using WPF.Dictionaries.Annotations;
using WPF.Dictionaries.Factories;

namespace WPF.Dictionaries.CustomForms
{
    public class BasicWindowViewModel: INotifyPropertyChanged
    {
        private IPresenterFactory _presenter;
        private IWorkerServices _dataServices;
        private bool? _dialogResult;
        private ICustomFormViewModel _customFormViewModel;
        private RelayCommand _okayCommand;

        public BasicWindowViewModel()
        {
            DialogResult = false;
        }

        public IPresenterFactory Presenter
        {
            get { return _presenter; }
            set
            {
                if (_presenter == value)
                    return;
                _presenter = value;
                NotifyPropertyChanged(nameof(Presenter));
            }
        }

        public IWorkerServices DataServices
        {
            get { return _dataServices; }
            set
            {
                if (_dataServices == value)
                    return;
                _dataServices = value;
                NotifyPropertyChanged(nameof(DataServices));
            }
        }

        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (value == _dialogResult) return;
                _dialogResult = value;
                NotifyPropertyChanged(nameof(DialogResult));
            }
        }

        public ICustomFormViewModel CustomFormViewModel
        {
            get { return _customFormViewModel; }
            set
            {
                if (Equals(value, _customFormViewModel)) return;
                _customFormViewModel = value;
                NotifyPropertyChanged(nameof(CustomFormViewModel));
            }
        }

        public RelayCommand OkayCommand => _okayCommand ?? (_okayCommand = new RelayCommand(() => DialogResult = true));

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
