using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    public class Researcher
    {
        public string familyName;
        public string givenName;

        public string title;
        public string email;
        public string school;

        public EmploymentLevel currentPosition;

        public Date commencedPositionDate;
        public Date commencedInstituteDate;

        //Tenure
        public int tenure;

        public float Q1percentage;

        //Generate Researcher Details
        public Researcher() {

        }
    }
}
