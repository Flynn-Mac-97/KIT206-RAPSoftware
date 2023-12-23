using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    class Student : Researcher
    {
        public String degree { get; set; }
        public Staff supervisor { get; set; }

        public Student() {
            

        }

        public Student(Researcher r) 
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

        public string supervisorName
        {
            get
            {
                return supervisor.givenName + " " + supervisor.familyName; 
            }
        }

        // returns the ID of the supervisor 
        public int supervisorID
        {
            get
            {
                return supervisor.id;
            }
        }

        /** an override for student object to string */
        public override string ToString()
        {
            return (
                $" Degree: {this.degree}\n" +
                $" Supervisor: {this.supervisorName}"
                );

        }

    }
}