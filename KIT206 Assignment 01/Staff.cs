using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace KIT206_Assignment_01
{
    class Staff : Researcher
    {
        public int FundingRecieved { get; set; }
        public List<Student> Supervisions { get; set; }

        //list of positions for this staff member
        public List<Position> positionHistory { get; set; }

        public Staff()
        {
            

        }

        //calculates the total number of publications in the previous three whole calendar years and divided by three.
        public float ThreeYearAVG()
        {
            float avg = 0;
            return avg;
        }

        //the metric of three years average 
        public float PublicationPerformance()
        {
            float performance = 0;
            return performance;
        }

        //returns the expected number of publications per years for corresponding level of employments.
        public float ExpectedPublications(EmploymentLevel e)
        {
            float expected = 0;

            switch (e)
            {
                case EmploymentLevel.RESEARCH_ASSOCIATE:
                    expected = 0.5f; break;
                case EmploymentLevel.LECTURER:
                    expected = 1; break;
                case EmploymentLevel.ASSISTANT_PROFESSOR:
                    expected = 2; break;
                case EmploymentLevel.ASSOCIATE_PROFESSOR:
                    expected = 3; break;
                case EmploymentLevel.PROFESSOR:
                    expected = 3.2f; break; 
            }

            return expected;
        }

        public override string ToString()
        {
            return (
                " Three Years Average: " + 
                ""
                );
        }
    }
}
