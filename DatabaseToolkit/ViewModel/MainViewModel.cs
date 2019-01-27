using DatabaseToolkit.Model;
using DatabaseToolkit.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<AssecoStuff> _leftDatabase = new ObservableCollection<AssecoStuff>();
        private ObservableCollection<AssecoStuff> _rightDatabase = new ObservableCollection<AssecoStuff>();
        public ObservableCollection<AssecoStuff> LeftDatabase { get { return _leftDatabase; } set { _leftDatabase = value; } }
        public ObservableCollection<AssecoStuff> RightDatabase { get { return _rightDatabase; } set { _rightDatabase = value; } }
        private Database _model;
        public CommandHandler MenuButton { get { return new CommandHandler(() => OpenMenuCommand(), true); }}
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

        private void OpenMenuCommand()
        {

        }

        public MainViewModel()
        {
            _model = new Database();
            var leftItems = _model.Read();
            foreach (var item in leftItems)
            {
                LeftDatabase.Add(item);
            }
        }

        private void OnPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
