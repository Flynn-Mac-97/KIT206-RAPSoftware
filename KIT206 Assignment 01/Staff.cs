using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    class Staff : Researcher
    {
        public float threeYearAVG { get; set; }
        public int fundingRecieved { get; set; }
        public float publicationPerformance { get; set; }
        public List<Student> supervisions { get; set; }
        public Staff(string fName, string lName, Title t, string email, string school, EmploymentLevel currentPos, Date posCommenced, Date instCommenced, int tenure, float Q1p) : base(fName, lName, t, email, school, currentPos, posCommenced, instCommenced, tenure, Q1p)
        {

        }
    }
}
