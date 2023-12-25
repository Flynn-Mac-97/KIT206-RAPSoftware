using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace KIT206_Assignment_01 {
    class Staff : Researcher {
        public int FundingRecieved { get; set; }
        //The list of students this staff supervises, can be empty.
        //Each researcher has an id remember, just store their ids, and find them in the list of all researchers when getting this list.

        // empty list of students to store the list of students that this staff is supervising. 
        public List<Student> supervisions = new List<Student>();


        public Staff() {

        }

        public Staff(Researcher r) {
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
        //go through the list of all researchers, find the students that have the same supervisorID as the staff
        public List<Student> Supervisions(List<Researcher> rlist) {
            foreach (Researcher r in rlist) {
                if (r.type == ResearcherType.STUDENT) {
                    //Student s = new Student(r);
                }
            }
            return new List<Student>();
        }

        // count the total number of student that the staff is supervising
        public int SupervisionCount {
            get {
                return supervisions.Count;
            }
        }

        //calculates the total number of publications in the previous three whole calendar years and divided by three.
        public float ThreeYearAVG {
            get {
                float count = 0;
                for (int i = 0; i <= 2; i++)
                {
                    count += this.PublicationsCountByYear((DateTime.Now.Year) - i);
                }
                return (float)count / 3;
            }
        }


        //a metric calculated using the average number of publications per year
        public int PublicationPerformance {
            get {
                return (int)(this.PublicationsCount / this.Tenure);
            }
        }

        //a metric calculated using the total amount of funding received per year
        public int FundingPerformance {
            get {
                float performance = 0;
                Researcher r = new Researcher();

                float tenure = r.Tenure;

                performance = FundingRecieved / tenure;

                return (int)performance;
            }
        }


        public override string ToString() {
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