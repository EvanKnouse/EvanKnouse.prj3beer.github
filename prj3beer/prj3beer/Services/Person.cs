using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    public class Person
    {
        #region Attributes
        private String email;
        private String password;
        private int age;
        private String name;
        private String gender;

        #endregion

        public Person()
        {
        }

        public Person(String sEmail, String sName, String sPassword, String sGender, int iAge)
        {
            this.email = sEmail;
            this.name = sName;
            this.password = sPassword;
            this.gender = sGender;
            this.age = iAge;
        }


        //Getters
        public String getEmail()
        {
            return this.email;
        }

        public String getName()
        {
            return this.name;
        }

        public String getPassword()
        {
            return this.password;
        }

        public String getGender()
        {
            return this.gender;
        }

        public int getAge()
        {
            return this.age;
        }


        //Setters
        public String setEmail(String newEmail)
        {
           return this.email = newEmail;
        }

        public String setName(String newName)
        {
            return this.name = newName;
        }

        public String setPassword(String newPassword)
        {
            return this.password = newPassword;
        }

        public String setGender(String newGender)
        {
            return this.gender = newGender;
        }

        public int setAge(int newAge)
        {
            return this.age = newAge;
        }
    }
}
