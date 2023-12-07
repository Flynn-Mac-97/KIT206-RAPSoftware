using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    public class Researcher
    {
        public string familyName { get; set; }
        public string givenName { get; set; }

        public string title { get; set; }
        public string email { get; set; }
        public string school { get; set; }

        public EmploymentLevel currentPosition { get; set; }

        public Date commencedPositionDate { get; set; }
        public Date commencedInstituteDate { get; set; }

        //Tenure
        public int tenure { get; set; }

        public float Q1percentage { get; set; }

        //Generate Researcher Details
        public Researcher() {

        }
    }
}
