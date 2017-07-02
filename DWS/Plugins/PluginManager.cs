using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DWS.Plugins
{
    public class PluginManager: INotifyPropertyChanged
    {
        private string startDirectory;
        public PluginManager(string directory)
        {
            startDirectory = directory;
            LoadPlugins();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadPlugins()
        {
            List<Assembly> allAssemblies = new List<Assembly>();
            foreach (string dllFolder in Directory.GetDirectories(startDirectory))
            {
                var _dllPath = Path.Combine(dllFolder, Path.GetFileName(dllFolder) + ".dll");
                var dll = Assembly.LoadFile( _dllPath );
                allAssemblies.Add( dll );
                foreach (Type _type in dll.GetExportedTypes())
                    if ( _type.GetTypeInfo().IsSubclassOf(typeof(AbstractPlugin)) )
                    {
                        Activator.CreateInstance(_type);
                        break;
                    }                
            }
        }
    }
}
