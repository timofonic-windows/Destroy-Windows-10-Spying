using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;

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

        public abstract string UIHeader{get;}

        /*Source you xaml settings UI*/
        public abstract object UIControl{get;}
    }

    public abstract class AbtractPlugin : IAbstractPlugin, INotifyPropertyChanged
    {
        private static ObservableCollection<AbtractPlugin> _instances = new ObservableCollection<AbtractPlugin>();
        /*Collect All plugins*/
        public static ObservableCollection<AbtractPlugin> Instances
        {
            get { return _instances;  }
        }
        protected AbtractPlugin()
        {
            foreach (AbtractPlugin _p in Instances )
                if (_p.Name == Name && _p.Version == Version)
                    throw new Exception("WTF?");
            Instances.Add(this);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public enum STATE { NONE, INIT, ApplyInProgress, RevokeInProgess};
        private STATE _state = STATE.NONE;
        /* Current work */
        public STATE State {
            get { return _state; }
            protected set
            {
                if ( value != _state)
                {
                    _state = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _progress = 0;
        /* 0 to 100. Pls. */
        public int Progress
        {
            get { return _progress; }
            protected set {
                if (_progress != value)
                {
                    _progress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _availableApply = false;
        /* Set true - if system come to apply you plugin */
        public bool AvailableApply {
            get { return _availableApply; }
            protected set {
                if ( _availableApply != value)
                {
                    _availableApply = value;
                    NotifyPropertyChanged();
                }
            } }

        private bool _availableRevoke = false;
        /* Set true - if system come to revoke plugin work */
        public bool AvailableRevoke
        {
            get { return _availableRevoke; }
            protected set
            {
                if (_availableRevoke != value)
                {
                    _availableRevoke = value;
                    NotifyPropertyChanged();
                }
            }
        }



        /* Start apply you plugin */
        public void Apply() {
            throw new NotImplementedException();
        }
        /* Restore all work of you plugin */
        public void Revoke() {
            throw new NotImplementedException();
        }

        public override string Name { get { return "Basic plugin"; } }
        public override string Version { get { return "0.0.1"; } }
        public override string Author { get { return "Unknown author < example@exampl.com >"; } }
        public override string UIHeader
        {
            get { return Name + " : " + Version; }
        }
    }
}
