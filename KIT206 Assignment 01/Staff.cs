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
                    //offset to -3 since we are in 2024 now and latest of them are 2021
                    count += this.PublicationsCountByYear(((DateTime.Now.Year)-1) - i);
                }
                return (float) count / 3 ;
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

                performance = GlobalXMLAdaptor.GetInstance(Globals.XmlFilePath).GetFundingForResearcher(this.id) / this.Tenure;

                return (int)performance;
            }
        }

        // returns the total funding recieved
        public int FundingRecievedCount {
            get {
                return GlobalXMLAdaptor.GetInstance(Globals.XmlFilePath).GetFundingForResearcher(this.id);
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
                float performance = this.ThreeYearAVG / ExpectedPublications * 100;

                //Console.WriteLine($"ThhreeYearAVG: {this.ThreeYearAVG}, ExpectedPublications: {ExpectedPublications}, Performance: {performance}");

                return performance;
            }
        }

        // returns performance level based on the performance measure value
        public ResearcherPerformance GetPerformanceLevel() {

            float total = this.CalculatePerformance;

            if (total <= 70)
                return ResearcherPerformance.POOR;
            else if (total > 70 && total < 110)
                return ResearcherPerformance.BELOW_EXPECTATIONS;
            else if (total >= 110 && total < 200)
                return ResearcherPerformance.MEETING_MINIMUM;
            else
                return ResearcherPerformance.STAR_PERFORMER;
        }

        //Return performance level as a string for display
        public string GetPerformanceLevelString() {
            switch (GetPerformanceLevel()) {
                case ResearcherPerformance.POOR:
                    return CalculatePerformance + ", Poor";
                case ResearcherPerformance.BELOW_EXPECTATIONS:
                    return CalculatePerformance + ", Below Expectations";
                case ResearcherPerformance.MEETING_MINIMUM:
                    return CalculatePerformance + ", Meeting Minimum";
                case ResearcherPerformance.STAR_PERFORMER:
                    return CalculatePerformance + ", Star Performer";
                default:
                    return "Error";
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

        public string GetSupervisions() {
            string s = "\n";
            foreach (Student st in supervisions) {
                s += st.givenName +" "+ st.familyName + "\n";
            }
            return s;
        }
    }
}