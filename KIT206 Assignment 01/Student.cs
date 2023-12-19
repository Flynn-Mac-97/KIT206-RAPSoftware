using System;

namespace KIT206_Assignment_01
{
    class Student : Researcher
    {
        public String degree { get; set; }
        public Staff supervisor { get; set; }

        public Student() {



        }


        /** an override for student object to string */
        public override string ToString()
        {
            return degree + ' ' + supervisor;
        }

    }
}