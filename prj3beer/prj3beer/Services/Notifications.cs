using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    class Notifications
    {
        public NotificationType LastNotification { get; set; }
        public bool NotificationSent { get; set; }

        private bool firstReading = true;

        string[] Title = { "Title Error", "Heat Warning", "Temperature Alert", "Drink Time!", "Temperature Alert", "Cold Warning" };
        string[] Body = { "Body Title", "Your beverage is getting too hot.", "Your beverage is just above the desired temperature.", "Your beverage has reached the perfect temperature.", "Your beverage is just below the desired temperature.", "Your beverage is getting too cold." };



        public int TryNotification(double receivedTemp, double idealTemp, NotificationType lastNotification)
        {
            NotificationType newNotification = CompareTemp(receivedTemp, idealTemp);

            if (CheckLastNotification(newNotification))
            {
                return (int)newNotification;
            }
            return 0;
        }

        

        private NotificationType CompareTemp(double receivedTemp, double idealTemp)
        {
            double dif = receivedTemp - idealTemp;

            NotificationType curType = default;
            

            if (dif == 0)
                curType = NotificationType.PERFECT;

            else if (dif == 1 || dif == 2 )
                curType = NotificationType.IN_RANGE_HOT;

            else if ((dif == -1 || dif == -2) && !(LastNotification > NotificationType.TOO_HOT && LastNotification < NotificationType.TOO_COLD))
                curType = NotificationType.IN_RANGE_COLD;

            else if (dif >= 4)
                curType = NotificationType.TOO_HOT;

            else if (dif <= -4)
                curType = NotificationType.TOO_COLD;

            return curType;

        }


        public bool CheckLastNotification(NotificationType current, NotificationType last)
        {

            if(current != last)
            {
                if (current == NotificationType.PERFECT && (last == NotificationType.IN_RANGE_HOT || last == NotificationType.IN_RANGE_COLD))
                    return false;
                else if ((current != NotificationType.TOO_COLD || current != NotificationType.TOO_HOT) && LastNotification != NotificationType.PERFECT)
                    return false;
                else;
                    return true;
            }
            return false;
            /*
            if (current == NotificationType.PERFECT && (last != NotificationType.PERFECT || last != NotificationType.IN_RANGE_HOT || last != NotificationType.IN_RANGE_COLD))
                return true;

            else if (current == NotificationType.IN_RANGE_COLD && (last != NotificationType.IN_RANGE_COLD || last != NotificationType.PERFECT))
                return true;

            else if (current == NotificationType.IN_RANGE_HOT && (last != NotificationType.IN_RANGE_HOT || last != NotificationType.PERFECT))
                return true;

            else if (current == NotificationType.TOO_COLD && last != NotificationType.TOO_COLD)
                return true;

            else if (current == NotificationType.TOO_HOT && last != NotificationType.TOO_HOT)
                return true;
                
            else
                return false;*/
        }
    }
}
