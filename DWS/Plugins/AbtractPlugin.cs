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
    public class AbtractPlugin : INotifyPropertyChanged
    {
        private SettingsPlugin _settings;
        public SettingsPlugin Settings
        {
            get;
            private set;
        }
        /*Collect All plugins*/
        private static ObservableCollection<AbtractPlugin> _instances;

        public static ObservableCollection<AbtractPlugin> Instances
        {
            get{ return _instances; }
        }
        AbtractPlugin()
        {
            foreach (AbtractPlugin _p in _instances)
                if (_p.Name == Name && _p.Version == Version)
                    throw new Exception("WTF?");
            _instances.Add(this);
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
        /* Name you plugin */
        public string Name { get { return "Basic plugin"; } }
        public string Version { get { return "0.0.1"; } }
        /* Its you. With email. May be... */
        public string Author { get { return "Unknown author < example@exampl.com >"; } }
        /* Start apply you plugin */
        public void Apply() {
            throw new NotImplementedException();
        }
        /* Restore all work of you plugin */
        public void Revoke() {
            throw new NotImplementedException();
        }

        public string UIHeader
        {
            get { return Name + " : " + Version; }
        }

        /*Source you xaml settings UI*/
        public string UIFrameSource
        {
            get { return Name + ".xaml"; }
        }

    }
}
