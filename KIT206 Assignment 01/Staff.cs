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

        public Staff(Researcher r)
        {
            this.id = r.id;
            this.type = r.type;
            this.givenName = r.givenName;
            this.familyName = r.familyName;
            this.title = r.title;
            this.unit = r.unit;
            this.campus = r.campus;
            this.email = r.email;
            this.photo = r.photo;
            this.level = r.level;
            this.utasStart = r.utasStart;
            this.currentStart = r.currentStart;
            this.publications = r.publications;
            this.positionHistory = r.positionHistory;
        }

        
        
        //list of student that a staff is supervising
        public List<Student> Supervisions 
        { 
            get 
            {
                List<Student> slist = new List<Student>();
                List<Researcher> rlist = new List<Researcher>();
                foreach(Researcher r in rlist)
                {
                    if (r.type  == ResearcherType.STUDENT)
                    {
                        Student s = new Student(r);
                        if(s.supervisorID == this.id)
                        {
                            slist.Add(s);
                        }
                    }
                }
                return slist;
            } 
        }
        
        // count the total number of student that the staff is supervising
        public int SupervisionCount
        {
            get 
            { 
                return Supervisions.Count; 
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

                performance = FundingRecieved / tenure; 

                return (int)performance;
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
