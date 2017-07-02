using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DWS.Plugins;
using System.IO;

namespace DWS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PluginManager Manager = new PluginManager(System.IO.Path.Combine( Directory.GetCurrentDirectory(), "Plugins" ) );
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine( Plugins.SimplePlugin.SimplePlugin.Instance.Name);
            Console.WriteLine("----------------------");
            foreach(AbstractPlugin _plugin in AbstractPlugin.Instances )
            {
                Console.WriteLine(_plugin.UIHeader);
            }
            Console.WriteLine("----------------------");
        }
    }
}
