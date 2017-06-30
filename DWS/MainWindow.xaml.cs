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

namespace DWS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine( Plugins.SimplePlugin.SimplePlugin.Instance.Name);
            Console.WriteLine("----------------------");
            foreach(AbtractPlugin _plugin in AbtractPlugin.Instances )
            {
                Console.WriteLine(_plugin.UIHeader);
            }
            Console.WriteLine("----------------------");
        }
    }
}
