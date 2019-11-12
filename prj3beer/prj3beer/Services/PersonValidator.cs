
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace prj3beer.Services
{
    public class PersonValidator
    {

        // Email regex found athttps://stackoverflow.com/questions/5342375/regex-email-validation
        private String emailRx = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
        private String nameRx = "^[a-zA-Z.]{1,30}$";
       // Password expression was take from http://regexlib.com/REDetails.aspx?regexp_id=1111
        private String passwordRx = "((?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\\W).{8,30})";
        private const int MIN_AGE = 18;
        private const int MAX_AGE = 150;
        private const int MIN_PASSWORD_LENGTH = 8;
        private const int MAX_PASSWORD_LENGTH = 30;
        private String[] GENDER_OPTIONS = { "male", "female", "other" };

        public PersonValidator()
        {
        }
        
        //public bool validateUser(Person person)
        //{
            
        //}

        public bool validateName(Person person)
        {
            if (Regex.IsMatch(person.getName(), nameRx))
            {
                return true;
            }
            return false;
        }

        public bool validateEmail(Person person)
        {
            if (Regex.IsMatch(person.getEmail(), emailRx))
            {
                return true;
            }
            return false;
        }

        public bool validatePassword(Person person)
        {
            if (Regex.IsMatch(person.getPassword(), passwordRx) && person.getPassword().Length <= 30)
            {
                return true;
            }
            return false;

        }

        public bool validateGender(Person person)
        {
            switch (person.getGender()) {

                case "male":
                    return true;
                case "female":
                    return true;
                case "other":
                    return true; 
                    default:
                    return false;



            };
          
        }

        public bool validateAge(Person person)
        {
            if (person.getAge() >= MIN_AGE && person.getAge() <= MAX_AGE)
            {
                return true;
            }
            return false;
        }





    }


}
