using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace prj3beer.ViewModels
{
    class StatusViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BindableProperty IdealTemperature { get; set; }
        public int Temperature { get; set; }
        public int BindingContext { get; }

        public StatusViewModel()
        {
            this.BindingContext = 1;
        }
    }
}
