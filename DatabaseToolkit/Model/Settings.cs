using DatabaseToolkit.Interface;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseToolkit.Model
{
    public class Settings:ISerialize<Settings>
    {
        public string LeftDBDataSource { get; set; }
        public string LeftDBUser { get; set; }
        public string LeftDBPassword { get; set; }
        public string LeftDBCatalog { get; set; }
        public string RightDBDataSource { get; set; }
        public string RightDBUser { get; set; }
        public string RightDBPassword { get; set; }
        public string RightDBCatalog { get; set; }
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Settings Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return new Settings();
                var fileContent = System.IO.File.ReadAllText(path);
                var settings = JsonConvert.DeserializeObject<Settings>(fileContent);
                return settings;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to load settings", ex);
                return new Settings();
            }
        }

        public bool Save(Settings data, string path)
        {
            try
            {
                var serializedObject = JsonConvert.SerializeObject(data);
                File.WriteAllText(path, serializedObject);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to save settings", ex);
                return false;
            }
            

        }
    }
}
