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
        

        public Staff()
        {
        }

        // count the total number of student that the staff is supervising
        public int SupervisionsCount
        {
            get 
            { 
                return Supervisions.Count; 
            }
        }
        
        //TO DO
        public List<Student> Supervisions 
        { 
            get 
            {
                return this.Supervisions;
            } 
        }

        //calculates the total number of publications in the previous three whole calendar years and divided by three.
        public float ThreeYearAVG
        {
            get
            {
                float count = 0;
                DateTime today = DateTime.Today;
                int year = today.Year;
                int three_years_ago = year - 3;

                Researcher r = new Researcher();
                List<Publication> plist = r.publications;


                foreach (Publication p in plist)
                {
                    if (three_years_ago >= p.yearPublished || p.yearPublished != year)
                    {
                        count++;
                    }
                }
                return count / 3;
            }
        }


        //a metric calculated using the average number of publications per year
        public int PublicationPerformance
        {
            get
            {
                float performance = 0; 
                Researcher r = new Researcher();

                float tenure = r.Tenure;
                int pubCount = r.PublicationsCount;

                performance = pubCount / tenure ;

                return (int)performance;
            }
        }

        //a metric calculated using the total amount of funding received per year
        public int FundingPerformance
        {
            get
            {
                float performance = 0;
                Researcher r = new Researcher();

                float tenure = r.Tenure;

                performance = FundingPerformance / tenure; 

                return (int)performance;
            }
        }

        //Counts the total number of students that a staff supervised
        public int SupervisionCount
        {
            get
            {
                return Supervisions.Count;
            }
        }

      
        public override string ToString()
        {
            return (
                " Three Years Average: " + ThreeYearAVG +
                "\n Funding Recieved: " + PublicationPerformance +
                "\n Performance by Publication" + PublicationPerformance +
                "\n Performance by Funding " + FundingPerformance +
                "\n Supervisions " + SupervisionCount 
                );
        }
    }
}
