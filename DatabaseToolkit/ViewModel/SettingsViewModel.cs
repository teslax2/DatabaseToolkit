using DatabaseToolkit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.ViewModel
{
    public class SettingsViewModel:BindableViewModel
    {
        private Settings _settings = new Settings();

        public String LeftDBDatasource { get { return _settings.LeftDBDataSource; } set { _settings.LeftDBDataSource = value; OnPropertyChanged("LeftDBDatasource"); } }
        public String LeftDBUser { get { return _settings.LeftDBUser; } set { _settings.LeftDBUser = value; OnPropertyChanged("LeftDBUser"); } }
        public String LeftDBPassword { get { return _settings.LeftDBPassword; } set { _settings.LeftDBPassword = value; OnPropertyChanged("LeftDBDPassword"); } }
        public String LeftDBCatalog { get { return _settings.LeftDBCatalog; } set { _settings.LeftDBCatalog = value; OnPropertyChanged("LeftDBCatalog"); } }
        public String RightDBDatasource { get { return _settings.RightDBDataSource; } set { _settings.RightDBDataSource = value; OnPropertyChanged("RightDBDatasource"); } }
        public String RightDBUser { get { return _settings.RightDBUser; } set { _settings.RightDBUser = value; OnPropertyChanged("RightDBUser"); } }
        public String RightDBPassword { get { return _settings.RightDBPassword; } set { _settings.RightDBPassword = value; OnPropertyChanged("RightDBDPassword"); } }
        public String RightDBCatalog { get { return _settings.RightDBCatalog; } set { _settings.RightDBCatalog = value; OnPropertyChanged("RightDBCatalog"); } }

        public CommandHandler SaveCommand { get { return new CommandHandler(() => SaveSettings(), true); } }

        private void SaveSettings()
        {
            _settings.Save(_settings, "settings.json");
        }

        private void LoadSettings()
        {
            _settings = _settings.Load("settings.json");
        }

        public SettingsViewModel()
        {
            LoadSettings();
        }
    }
}
