using System.Windows;
using Dacha.DataModel;
using Dacha.DataModel.NHibernate.Domain;
using Dacha.Inspector.Dictionaries;
using Dacha.Inspector.Factories;
using Dacha.WPFUtils;

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
        private Database _db;
        private IPresenterFactory _presenter;


        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(Exit));

        public RelayCommand SectorCommand => _sectorCommand ?? (_sectorCommand = new RelayCommand(OpenSector));

        private void OpenDictionary<T>() where T : new()
        {
            _db.WorkWithList<T>((refresher, saver, deleter) =>
            {
                var dictionaryViewModel = new DictionaryViewModel<T>
                {
                    Presenter = _presenter,
                    DictionaryGetter = refresher,
                    DictionaryAdder = saver,
                    DictionaryDeleter = deleter
                };
                _presenter.PresentDictionary<T>(dictionaryViewModel);
            });
        }

        private void OpenSector()
        {
            OpenDictionary<Sector>();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
