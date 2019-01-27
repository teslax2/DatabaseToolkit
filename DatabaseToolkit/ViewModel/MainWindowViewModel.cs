using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.ViewModel
{
    class MainWindowViewModel : BindableViewModel
    {
        private BindableViewModel _currentViewModel;
        public BindableViewModel CurrentViewModel { get { return _currentViewModel;} set { _currentViewModel = value; OnPropertyChanged("CurrentViewModel"); } }

        private DataTablesViewModel _dataTablesViewModel = new DataTablesViewModel();
        private SettingsViewModel _settingsViewModel = new SettingsViewModel();

        public CommandHandler Navigation { get { return new CommandHandler(s => OnNav(s), true); } }

        public MainWindowViewModel()
        {
            CurrentViewModel = _dataTablesViewModel;
        }

        private void OnNav(object destination)
        {
            string viewName = destination as string;
            switch (viewName)
            {
                case "settings":
                    CurrentViewModel = _settingsViewModel;
                    break;
                case "tables":
                default:
                    CurrentViewModel = _dataTablesViewModel;
                    break;
            }
        }
    }
}
