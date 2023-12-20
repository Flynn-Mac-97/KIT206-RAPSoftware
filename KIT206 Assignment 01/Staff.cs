using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace KIT206_Assignment_01
{
    class Staff : Researcher
    {
        public int FundingRecieved { get; set; }
        public List<Student> Supervisions { get; set; }
        public int SupervisionCount; 

        //list of positions for this staff member
        public List<Position> positionHistory { get; set; }

        public Staff()
        {
            SupervisionCount = SupervisionsCount();


        }

        // count the total number of student that the staff is supervising
        public int SupervisionsCount() 
        {
            int count = 0;
            foreach (Student s in Supervisions)
            {
                count++;
            }
            return count; 
        }

        //calculates the total number of publications in the previous three whole calendar years and divided by three.
        public float ThreeYearAVG(List<Publication> plist)
        {
            float avg = 0;
            int count = 0;
            DateTime today = DateTime.Today;
            int year = today.Year;
            int three_years_ago = year - 3;

            foreach (Publication p in plist) 
            {
                if (three_years_ago >= p.yearPublished || p.yearPublished != year) 
                {
                    count++;
                }
            }

            avg = count / 3; 
            return avg;
        }

        //the metric of three years average 
        public float PublicationPerformance(List<Publication> list)
        {
            float performance = 0;
            DateTime today = DateTime.Today;


            return performance;
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
