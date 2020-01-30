using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace prj3beer.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool masterNotifications;

        bool MasterNotifications
        {
            set
            {
                if (masterNotifications != value)
                {
                    masterNotifications = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("MasterNotifications");
                    }
                }
            }
            get
            {
                return masterNotifications;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
