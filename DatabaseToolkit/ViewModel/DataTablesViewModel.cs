using DatabaseToolkit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.ViewModel
{
    public class DataTablesViewModel:BindableViewModel
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<AssecoStuff> _leftDatabase = new ObservableCollection<AssecoStuff>();
        private ObservableCollection<AssecoStuff> _rightDatabase = new ObservableCollection<AssecoStuff>();
        public ObservableCollection<AssecoStuff> LeftDatabase { get { return _leftDatabase; } set { _leftDatabase = value; } }
        public ObservableCollection<AssecoStuff> RightDatabase { get { return _rightDatabase; } set { _rightDatabase = value; } }
        private Database _model;
        public CommandHandler CopyFromLeft { get { return new CommandHandler(() => CopyFromLeftCommand(), true); } }
        public CommandHandler CopyFromRight { get { return new CommandHandler(() => CopyFromRightCommand(), true); } }
        private ObservableCollection<AssecoStuff> _selectedItems = new ObservableCollection<AssecoStuff>();
        public ObservableCollection<AssecoStuff> SelectedItems { get { return _selectedItems; } set { _selectedItems = value; } }

        private void CopyFromRightCommand()
        {
            _log.Info(SelectedItems.Count);
        }

        private void CopyFromLeftCommand()
        {
            _log.Info(SelectedItems.Count);
        }

        public DataTablesViewModel()
        {
            _model = new Database();
            var leftItems = _model.Read();
            foreach (var item in leftItems)
            {
                LeftDatabase.Add(item);
            }
        }
    }
}

