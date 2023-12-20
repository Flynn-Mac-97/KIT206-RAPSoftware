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
        public int publications { get; set; }
        public int q1publications { get; set; }
        public float q1percentage { get; set; }
        public string imageURL { get; set; }




        public Researcher()
        {
            publications = 0;
        }
        
        // get a current job details from position
      
        // returns Q1 percentage
        public float Q1percentage()
        {
            if (publications == 0)
            {
                return 0;
            }

            q1percentage = (float) q1publications / publications * 100;
            
            return q1percentage;

        }

        // returns the total publications
        public int PublicationsCount()
        {
            foreach(Publication p in plist)
            {
                this.publications++;
            }
            
            return this.publications;

        }

        // returns tenure
        public float Tenure()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan tenureSpan = currentDate - utasStart;

            tenure = (float)tenureSpan.TotalDays / 365;

            return tenure;

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
