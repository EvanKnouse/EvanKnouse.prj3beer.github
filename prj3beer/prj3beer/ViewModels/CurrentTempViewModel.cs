using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using prj3beer.Services;

namespace prj3beer.ViewModels
{
    /// <summary>
    /// A class implementing INotifyPropertyChanged to bind the read temperature values to an element on the
    /// Status page.
    /// </summary>
    public class CurrentTempViewModel : INotifyPropertyChanged
    {
        double currentTemp;
        INotificationHandler nh;

        public event PropertyChangedEventHandler PropertyChanged;

        public CurrentTempViewModel()
        {
            //Checks for update temps every second.  Will eventually poll an object associated with a
            //bluetooth reading.  Currently communicates with a class bouncing between -35 and 35 degrees celsius.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.CurrentTemp = MockTempReadings.Temp;
                //nh.CompareTemp(this.CurrentTemp, this._temperature);
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
                        //If the property has changed, fire an event.
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