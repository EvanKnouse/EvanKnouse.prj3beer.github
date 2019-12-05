using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using prj3beer.Services;

namespace prj3beer.ViewModels
{
    public class CurrentTempViewModel : INotifyPropertyChanged
    {
        double currentTemp;

        public event PropertyChangedEventHandler PropertyChanged;

        public CurrentTempViewModel()
        {
            this.currentTemp = 20.0;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.CurrentTemp -= 1.0;
                return true;
            });
        }

        public double CurrentTemp
        {
            set
            {
                if (currentTemp != value)
                {
                    currentTemp = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentTemp"));
                    }
                }
            }
            get
            {
                return currentTemp;
            }
        }
    }
}