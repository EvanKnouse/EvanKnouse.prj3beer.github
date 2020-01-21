using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    public interface INotificationHandler
    {
        void Initialize();

        void SendLocalNotification(string title, string body);

        //bool CompareTemp(double receivedTemp, double idealTemp);
    }
}