using System.ComponentModel;
using System.Runtime.CompilerServices;
using BasicDataStructures.Interfaces;
using WPF.Dictionaries.Annotations;
using WPF.Dictionaries.Factories;

namespace WPF.Dictionaries.CustomForms
{
    public class BasicCustomFormViewModel: ICustomFormViewModel, INotifyPropertyChanged
    {
        private IPresenterFactory _presenter;
        private IWorkerServices _dataServices;
        private bool? _dialogResult;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
