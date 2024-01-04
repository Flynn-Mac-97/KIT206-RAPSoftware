using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    class Student : Researcher
    {
        public string degree { get; set; }
        public Staff supervisor { get; set; }

        public Student() {
            
        }

        public string supervisorName
        {
            get
            {
                return supervisor.givenName + " " + supervisor.familyName; 
            }
        }

        // returns the ID of the supervisor 
        public int supervisorID
        {
            get
            {
                return supervisor.id;
            }
        }

        /** an override for student object to string */
        public override string ToString()
        {
            //Return all the generic details of this student
            return base.ToString() + " " + degree + " " + supervisorName;
        }

    }
}