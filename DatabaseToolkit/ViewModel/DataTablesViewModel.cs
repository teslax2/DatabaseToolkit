using DatabaseToolkit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseToolkit.ViewModel
{
    public class DataTablesViewModel:BindableViewModel
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<AssecoStuff> _leftDatabase = new ObservableCollection<AssecoStuff>();
        private ObservableCollection<AssecoStuff> _rightDatabase = new ObservableCollection<AssecoStuff>();
        private int _copiedItems = 0;
        public int CopiedItems { get { return _copiedItems; } set { _copiedItems = value; OnPropertyChanged("CopiedItems"); } }
        public ObservableCollection<AssecoStuff> LeftDatabase { get { return _leftDatabase; } set { _leftDatabase = value; } }
        public ObservableCollection<AssecoStuff> RightDatabase { get { return _rightDatabase; } set { _rightDatabase = value; } }
        public CommandHandler CopyFromLeft { get { return new CommandHandler(selectedRows => CopyFromLeftCommand(selectedRows), true); } }
        public CommandHandler CopyFromRight { get { return new CommandHandler(selectedRows => CopyFromRightCommand(selectedRows), true); } }
        public CommandHandler ReloadButton { get { return new CommandHandler(() => ReloadDatabase(), true); } }

        private Database _leftDB;
        private Database _rightDB;
        private Settings _settings;
              
        private void CopyFromRightCommand(object selectedRows)
        {
            if (selectedRows == null)
                return;
            var items = selectedRows as List<AssecoStuff>;
            CopiedItems=_leftDB.Update(items);
            UpdateDataGrids();
        }

        private void CopyFromLeftCommand(object selectedRows)
        {
            if (selectedRows == null)
                return;
            var items = selectedRows as List<AssecoStuff>;
            CopiedItems=_rightDB.Update(items);
            UpdateDataGrids();
        }

        public DataTablesViewModel()
        {
            ReloadDatabase();
        }

        private void ReloadDatabase()
        {
            _settings = new Settings();
            var parameters = _settings.Load();
            _leftDB = new Database(parameters.LeftDBDataSource, parameters.LeftDBUser, parameters.LeftDBPassword, parameters.LeftDBCatalog);
            _rightDB = new Database(parameters.RightDBDataSource, parameters.RightDBUser, parameters.RightDBPassword, parameters.RightDBCatalog);
            UpdateDataGrids();
        }

        private void UpdateDataGrids()
        {
            var leftItems = _leftDB.Read();
            var rightItems = _rightDB.Read();
            LeftDatabase.Clear();
            foreach (var item in leftItems)
            {
                LeftDatabase.Add(item);
            }
            RightDatabase.Clear();
            foreach (var item in rightItems)
            {
                RightDatabase.Add(item);
            }
        }

    }
}

