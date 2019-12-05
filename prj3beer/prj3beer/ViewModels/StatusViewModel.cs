using System.ComponentModel;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        double? _prefTemp;

        bool _isCelsius;

        public string Scale
        {
            get
            {
                return _isCelsius ? "\u00B0C" : "\u00B0F";
            }
        }
        public double Minimum
        {
            get
            {
                return _isCelsius ? -30 : -22;
            }
        }
        public double Maximum
        {
            get
            {
                return _isCelsius ? 30 : 86;
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

        public bool IsCelsius
        {
            get { return _isCelsius; }
            set { _isCelsius = value; }
        }

        public bool IsCelsiusUpdate
        {
            get { return _isCelsius; }

            set
            {
                _isCelsius = value;
                OnPropertyChanged("IsCelsiusUpdate");
                /*try
                {
                    isCelsius = value;
                }
                catch
                {
                    isCelsius = null;
                }
                finally
                {
                    OnPropertyChanged("IsCelsiusUpdate");
                }*/
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
