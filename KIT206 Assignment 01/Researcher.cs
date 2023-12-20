using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    public class Researcher
    { 
        public int id { get; set; } 
        public string familyName { get; set; }
        public string givenName { get; set; }
        public Title title { get; set; }
        public EmploymentLevel level { get; set; }
        public string email { get; set; }
        public string school { get; set; }
        public string campus { get; set; }
        public DateTime utasStart { get; set; }
        public DateTime positionStart { get; set; }
        public float tenure { get; set; }
        public float q1percentage { get; set; }
        public string imageURL { get; set; }
        public List<Publication> publications { get; set; }




        public Researcher()
        {

        }

        // returns the total publications
        public int publicationsCount
        {

            get { return publications.Count; }

        }

        // returns the tenure years
        public float Tenure
        {
            get
            {
                DateTime currentDate = DateTime.Now;
                TimeSpan tenureSpan = currentDate - utasStart;

                tenure = (float)tenureSpan.TotalDays / 365;

                return tenure;

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

                return (float) count / publicationsCount * 100;

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
                $" School: {this.school}\n" +
                $" Commenced at Position: {this.positionStart.Date.ToString()}\n" +
                $" Commenced at Institute: {this.utasStart.Date.ToString()}\n" +
                $" Tenure: {this.tenure}," +
                $" Q1: {this.q1percentage}"
                );
        }
    }
}
