using System;

namespace KIT206_Assignment_01
{
    class Student : Researcher
    {
        public String degree { get; set; }
        public Staff supervisor { get; set; }

        public Student(string fName, string lName, Title t, string email, string school, EmploymentLevel currentPos, Date posCommenced, Date instCommenced, int tenure, float Q1p) : base(fName, lName, t, email, school, currentPos, posCommenced, instCommenced, tenure, Q1p)
        {

        }

    }
}