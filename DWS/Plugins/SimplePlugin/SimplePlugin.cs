using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWS.Plugins.SimplePlugin
{
    public class SimplePlugin:AbstractPlugin
    {
        public override string Name => "SimplePlugin"; 
        public override string Author => "No author, lol";
        public override string Version => "0.0.0";

        private object _ui = new SimplePlugin_ui();
        public override object UIControl => _ui;

        private static SimplePlugin _instance = new SimplePlugin();

        public static SimplePlugin Instance
        {
            get
            {
                return _instance;
            }
        }

        SimplePlugin()
            :base()
        {
        }
    }
}
