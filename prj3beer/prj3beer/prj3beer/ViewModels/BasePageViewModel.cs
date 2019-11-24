using prj3beer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace prj3beer.ViewModels
{
    public class BasePageViewModel 
    {

        public bool IsBust { get; set; }
        public IAPIManager Api { get; set; }


        public BasePageViewModel()
        {
            Api = new APIMockManager();

            //Uncomment to use real API!
            //Api = new ApiManager();
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
