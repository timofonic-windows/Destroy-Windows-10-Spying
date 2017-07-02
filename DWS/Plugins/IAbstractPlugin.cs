using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWS.Plugins
{
    public abstract class IAbstractPlugin
    {
        /* Name you plugin */
        public abstract string Name { get; }
        /*You version */
        public abstract string Version { get; }
        /* Its you. With email. May be... */
        public abstract string Author { get; }

        public abstract string Description { get; }

        public abstract string UIHeader { get; }

        /*Source you xaml settings UI*/
        public abstract object UIControl { get; }
    }
}
