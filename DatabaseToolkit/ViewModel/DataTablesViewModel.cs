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
        private int _copiedItemsFromLeft = 0;
        public int CopiedItemsFromLeft { get { return _copiedItemsFromLeft; } set { _copiedItemsFromLeft = value; OnPropertyChanged("CopiedItemsFromLeft"); } }
        private int _copiedItemsFromRight = 0;
        public int CopiedItemsFromRight { get { return _copiedItemsFromRight; } set { _copiedItemsFromRight = value; OnPropertyChanged("CopiedItemsFromRight"); } }
        public ObservableCollection<AssecoStuff> LeftDatabase { get { return _leftDatabase; } set { _leftDatabase = value; } }
        public ObservableCollection<AssecoStuff> RightDatabase { get { return _rightDatabase; } set { _rightDatabase = value; } }
        public CommandHandler CopyFromLeft { get { return new CommandHandler(async(selectedRows) => await CopyFromLeftCommand(selectedRows), true); } }
        public CommandHandler CopyFromRight { get { return new CommandHandler(async(selectedRows) => await CopyFromRightCommand(selectedRows), true); } }
        public CommandHandler ReloadButton { get { return new CommandHandler(async() => await ReloadDatabase(), _isReady); } }
        private bool _isReady = true;
        public bool IsReady { get { return _isReady; } set { _isReady = value; OnPropertyChanged("IsReady"); } }

        private Database _leftDB;
        private Database _rightDB;
        private Settings _settings;
              
        private async Task CopyFromRightCommand(object selectedRows)
        {
            if (selectedRows == null)
                return;
            System.Collections.IList items = (System.Collections.IList)selectedRows;
            var collection = items.Cast<AssecoStuff>();
            CopiedItemsFromRight = await _leftDB.UpdateAsync(collection.ToList(),false);
            CopiedItemsFromLeft = 0;
            await UpdateDataGrids();
        }

        private async Task CopyFromLeftCommand(object selectedRows)
        {
            if (selectedRows == null)
                return;
            System.Collections.IList items = (System.Collections.IList)selectedRows;
            var collection = items.Cast<AssecoStuff>();
            CopiedItemsFromLeft=await _rightDB.UpdateAsync(collection.ToList(),false);
            CopiedItemsFromRight = 0;
            await UpdateDataGrids();
        }

        public DataTablesViewModel()
        {
        }

        private async Task ReloadDatabase()
        {
            _settings = new Settings();
            var parameters = _settings.Load();
            _leftDB = new Database(parameters.LeftDBDataSource, parameters.LeftDBUser, parameters.LeftDBPassword, parameters.LeftDBCatalog);
            _rightDB = new Database(parameters.RightDBDataSource, parameters.RightDBUser, parameters.RightDBPassword, parameters.RightDBCatalog);
            await _leftDB.SeedAsync();
            await UpdateDataGrids();
        }

        private async Task UpdateDataGrids()
        {
            IsReady = false;
            var leftItems = await _leftDB.ReadAsync();
            var rightItems = await _rightDB.ReadAsync();
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
            IsReady = true;
        }

    }
}

