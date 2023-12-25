using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    public class Researcher
    { 
        public int id { get; set; } 
        public ResearcherType type { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public Title title { get; set; }
        public string unit { get; set; }
        public Campus campus { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public EmploymentLevel level { get; set; }
        public DateTime utasStart { get; set; }
        public DateTime currentStart { get; set; }
        
        public List<Publication> publications { get; set; }
        public List<Position> positionHistory { get; set; }

        public Researcher()
        {
        }

        // returns the total publications
        public int PublicationsCount
        {

            get { 
                if(this.publications == null) {
                    return 0;
                }
                else {
                    return this.publications.Count;
                }
            }
        }
        //Count publications by year
        public int PublicationsCountByYear(int year) {
            int count = 0;
            foreach (Publication p in publications) {
                if (p.yearPublished == year) {
                    count++;
                }
            }
            return count;
        }

        //Get oldest publication year
        public int OldestPublicationYear() {
            int oldest = 0;
            foreach (Publication p in publications) {
                if (p.yearPublished < oldest) {
                    oldest = p.yearPublished;
                }
            }
            return oldest;
        }

        //Get list of counts of publications by year
        public List<string> PublicationsCountByYear() {
            List<string> counts = new List<string>();
            for (int i = DateTime.Now.Year; i >= OldestPublicationYear(); i--) {
                counts.Add(i+" : "+PublicationsCountByYear(i).ToString());
            }
            return counts;
        }

        // returns the tenure years
        public float Tenure
        {
            get
            {
                DateTime currentDate = DateTime.Now;
                TimeSpan tenureSpan = currentDate - utasStart;

                return (float) tenureSpan.TotalDays / 365;

            }
        }


        // returns Q1 percentage
        public float Q1percentage 
        { 
            get {

                float count = 0;
                foreach (Publication p in publications)
                {
                    if ( p.ranking == Ranking.Q1 )
                    {
                        count++;
                    }
                    
                }

                return (float) count / PublicationsCount * 100;

            }

        }

        // gets current job details 
        public Position GetCurrentJob()
        {
            DateTime currentDate = DateTime.Now;

            foreach (Position p in positionHistory)
            {
                if (p.startDate <= currentDate && p.endDate > currentDate)
                {
                    return p;
                }
            }

            return null;
        }

        // returns current job title
        public string currentJobTitle
        {
            get {

                Position currentJob = GetCurrentJob();

                return currentJob.Title();
 
            }
        }

        // returns current job start date
        public DateTime CurrentJobStart
        {
            get
            {
                Position currentJob = GetCurrentJob();

                return currentJob.startDate;
            }

        }

        // gets earliest job details 
        public Position GetEarliestJob()
        {
            DateTime currentDate = DateTime.Now;

            foreach (Position p in positionHistory)
            {
                if (p.startDate < currentDate && p.endDate < currentDate)
                {
                    return p;
                }
            }

            return null;
        }

        // returns earliest job start date
        public DateTime EarliestJobStart
        {
            get
            {
                Position earliestJob = GetEarliestJob();

                return earliestJob.startDate;
            }

        }

        //an override for researcher object to string;
        public override string ToString()
        {
            return (
                $"{this.familyName}," +
                $" {this.givenName}," +
                $" {this.title}\n" +
                $" Email: {this.email}\n" +
                $" School: {this.unit}\n" +
                $" Commenced at Position: {this.currentStart.Date.ToString()}\n" +
                $" Commenced at Institute: {this.utasStart.Date.ToString()}\n" +
                $" Tenure: {this.Tenure}," +
                $" Q1: {this.Q1percentage}"
                );
        }
    }
}
