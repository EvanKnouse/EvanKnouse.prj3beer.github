using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace prj3beer.Models
{
    class BoundIdealTemp : BindableObject
    {
        public static readonly BindableProperty IdealTemperature = BindableProperty.Create("IdealTemp", typeof(double), typeof(Beverage), default(double));

        public double IdealTemp {
            get { return (double)GetValue(IdealTemperature); }
            set {
                try
                {
                    SetValue(IdealTemperature, value);
                }
                catch( ArgumentException e )
                {

                }
            }
        }
    }
}