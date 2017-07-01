using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostsEditor
{
    public class PluginMain
    {

        public const string _Description = @"Default plugin for add spy domains to hosts file.";
        public const string _PluginName = @"Disable spy hosts plugin";
        
        public PluginMain()
        {
#if DEBUG
            MessageBox.Show(@"Plugin Loaded");
#endif
        }

        public void PluginStart()
        {
#if DEBUG
            MessageBox.Show(@"Plugin Started");
#endif
        }

    }
}
