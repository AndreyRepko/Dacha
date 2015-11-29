using System.Windows;
using Dacha.DataModel;
using Dacha.DataModel.NHibernate.Domain;
using Dacha.WPFUtils;
using WPF.Dictionaries.Dictionaries;
using WPF.Dictionaries.Factories;

namespace Dacha.Inspector
{
    public class AWPInspectorViewModel
    {
        public AWPInspectorViewModel()
        {
            _db = new Database();
            _presenter = new PresenterFactory();
        }

        private RelayCommand _exitCommand;
        private RelayCommand _sectorCommand;
        private readonly Database _db;
        private readonly IPresenterFactory _presenter;
        private RelayCommand _placesCommand;

        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(Exit));

        public RelayCommand SectorCommand => _sectorCommand ?? (_sectorCommand = new RelayCommand(OpenSector));

        public RelayCommand PlacesCommand => _placesCommand ?? (_placesCommand = new RelayCommand(OpenPlaces));

        private void OpenDictionary<T>() where T : new()
        {
            _db.WorkWithList<T>((refresher, saver, deleter) =>
            {
                var dictionaryViewModel = new DictionaryViewModel<T>
                {
                    Presenter = _presenter,
                    DictionaryGetter = refresher,
                    DictionarySaver = saver,
                    DictionaryDeleter = deleter
                };
                _presenter.PresentDictionary<T>(dictionaryViewModel);
            });
        }

        private void OpenSector()
        {
            OpenDictionary<Sector>();
        }

        private void OpenPlaces()
        {
            OpenDictionary<Places>();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
