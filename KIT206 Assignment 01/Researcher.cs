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
        public string email { get; set; }
        public string school { get; set; }
        public string campus { get; set; }
        public DateTime utasStart { get; set; }
        public DateTime positionStart { get; set; }
        public int tenure { get; set; }
        public float Q1percentage { get; set; }
        public string imageURL { get; set; }

        public Researcher()
        {

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
                $" Q1: {this.Q1percentage}"
                );
        }
    }
}
