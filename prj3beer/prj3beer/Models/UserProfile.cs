using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Plugin.GoogleClient.Shared;

namespace prj3beer.Models
{
    public class UserProfile : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserProfile()
        {
            Name = Settings.CurrentUserName;
            Email = Settings.CurrentUserEmail;
        }
    }
}
