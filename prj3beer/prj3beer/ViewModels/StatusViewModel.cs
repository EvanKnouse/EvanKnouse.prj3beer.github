using System.ComponentModel;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    class StatusViewModel : INotifyPropertyChanged
    {
        double? _prefTemp;

        public bool isCelsius { get; set; }

        public string Scale
        {
            get
            {
                return isCelsius ? "\u00B0C" : "\u00B0F";
            }
        }
        public double Minimum
        {
            get
            {
                return isCelsius ? -30 : -22;
            }
        }
        public double Maximum
        {
            get
            {
                return isCelsius ? 30 : 86;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double? PreferredTemperature
        {
            get { return _prefTemp; }
            set { _prefTemp = value; }
        }

        public string PreferredTemperatureString
        {
            get { return _prefTemp.HasValue ? _prefTemp.ToString() : ""; }

            set
            {
                try
                {
                    _prefTemp = double.Parse(value);
                }
                catch
                {
                    _prefTemp = null;
                }
                finally
                {
                    OnPropertyChanged("PreferredTempString");
                }
            }
        }


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
