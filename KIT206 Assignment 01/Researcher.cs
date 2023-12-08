using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    public class Researcher
    {
        public string familyName { get; set; }
        public string givenName { get; set; }

        public Title title { get; set; }
        public string email { get; set; }
        public string school { get; set; }

        public EmploymentLevel currentPosition { get; set; }

        public Date commencedPositionDate { get; set; }
        public Date commencedInstituteDate { get; set; }

        //Tenure
        public int tenure { get; set; }

        public float Q1percentage { get; set; }

        //Constructor
        public Researcher(string fName, string lName, Title t, string email, string school, EmploymentLevel currentPos, Date posCommenced, Date instCommenced,  int tenure, float Q1p)
        {
            this.familyName = fName;
            this.givenName = lName;
            this.title = t;
            this.email = email;
            this.school = school;
            this.currentPosition = currentPos;
            this.commencedPositionDate = posCommenced;
            this.commencedInstituteDate = instCommenced;
            this.tenure = tenure;
            this.Q1percentage = Q1p;
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
                $" {this.currentPosition}\n" +
                $" Commenced at Position: {this.commencedPositionDate.ToString()}\n" +
                $" Commenced at Institute: {this.commencedInstituteDate.ToString()}\n" +
                $" Tenure: {this.tenure}," +
                $" Q1: {this.Q1percentage}"
                );
        }
    }
}
