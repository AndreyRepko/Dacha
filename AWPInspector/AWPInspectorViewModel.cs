using System.Windows;
using Dacha.DataModel;
using Dacha.DataModel.NHibernate.Domain;
using Dacha.Inspector.Dictionaries;
using Dacha.WPFUtils;

namespace Dacha.Inspector
{
    public class AWPInspectorViewModel
    {
        public AWPInspectorViewModel()
        {
            _db = new Database();
        }

        private RelayCommand _exitCommand;
        private RelayCommand _sectorCommand;
        private Database _db;
        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(Exit));

        public RelayCommand SectorCommand => _sectorCommand ?? (_sectorCommand = new RelayCommand(OpenSector));

        private void OpenDictionary<T>()
        {
            _db.WorkWithList<T>((list) =>
            {
                var t = new DictionaryWindow();
                var l = new DictionaryViewModel<T> {Dictionary = list};
                t.DataContext = l;
                t.ShowDialog();
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
