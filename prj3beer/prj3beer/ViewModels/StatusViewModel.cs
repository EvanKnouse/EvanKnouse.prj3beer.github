using System.ComponentModel;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        double? _temperature;

        public bool isCelsius { get; set; }

        public string Scale { get { return isCelsius ? "\u00B0C" : "\u00B0F"; } }

        public double Minimum { get { return isCelsius ? -30 : -22; } }
        
        public double Maximum { get { return isCelsius ?  30 :  86; } }

        public double? Temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }

        public string PreferredTemperatureString
        {
            get { return _temperature.HasValue ? _temperature.ToString() : ""; }

            set
            {
                try
                {
                    _temperature = double.Parse(value);
                }
                catch
                {
                    _temperature = null;
                }
                finally
                {
                    OnPropertyChanged("PreferredTempString");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
