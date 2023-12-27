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

        //For now just set to POOR , but we will calculate when checking performance.
        public ResearcherPerformance performance { get; set; }

        public Staff() {

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
                //Researcher r = new Researcher();

                float tenure = this.Tenure;

                performance = GlobalXMLAdaptor.GetInstance(Globals.XmlFilePath).GetFundingForResearcher(this.id) / tenure;

                return (int)performance;
            }
        }

        // calculates expected number of publications
        public float ExpectedPublications {
            get {
                float expectedPublications = 0;

                switch (this.level) {
                    case EmploymentLevel.RESEARCH_ASSOCIATE:
                        expectedPublications = 0.5f;
                        break;
                    case EmploymentLevel.LECTURER:
                        expectedPublications = 1;
                        break;
                    case EmploymentLevel.ASSISTANT_PROFESSOR:
                        expectedPublications = 2;
                        break;
                    case EmploymentLevel.ASSOCIATE_PROFESSOR:
                        expectedPublications = 3.2f;
                        break;
                    case EmploymentLevel.PROFESSOR:
                        expectedPublications = 4;
                        break;
                }

                return expectedPublications;

            }

        }


        // calculates performance measure
        public float CalculatePerformance {
            get {
                float performance = (this.ThreeYearAVG / ExpectedPublications) * 100;

                return performance;
            }
        }

        // returns performance level based on the performance measure value
        public ResearcherPerformance InitialiseStaffFundingPerformance() {

            float total = CalculatePerformance;

            if (total <= 70)
                return ResearcherPerformance.POOR;
            else if (total > 70 && total < 110)
                return ResearcherPerformance.BELOW_EXPECTATIONS;
            else if (total >= 110 && total < 200)
                return ResearcherPerformance.MEETING_MINIMUM;
            else
                return ResearcherPerformance.STAR_PERFORMER;
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