using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01 {
    internal class FlynnsWhiteboxTesting {

        public void TestDetailsAreLoaded() {
            //find first researcher view model where level is "student"
            var student = ResearchController.Instance.ResearcherNames.First(r => r.Level == "Student");
            var staff = ResearchController.Instance.ResearcherNames.First(r => r.Level == "Assistant Professor");

            //Load it fully from db.
            Researcher studentFromDB = ResearchController.Instance.LoadResearcher(student.ID);
            Researcher staffFromDB = ResearchController.Instance.LoadResearcher(staff.ID);
            string[] studentGenericDetails = ResearchController.Instance.GenericResearcherDetails(studentFromDB);
            string[] studentSpecificDetails = ResearchController.Instance.SpecificResearcherDetails(studentFromDB);
            string[] staffSpecificDetails = ResearchController.Instance.SpecificResearcherDetails(staffFromDB);
            string[] staffGenericDetails = ResearchController.Instance.GenericResearcherDetails(staffFromDB);
            string[] studentUniqueDetails = ResearchController.Instance.StaffOrStudentSpecificDetails(studentFromDB);
            string[] staffUniqueDetails = ResearchController.Instance.StaffOrStudentSpecificDetails(staffFromDB);

            Console.WriteLine("Testing details for student: " + student.ToString());
            Console.WriteLine("Image for this student is: " + studentFromDB.photo);
            //loop through and print generic details
            foreach (string detail in studentGenericDetails) {
                Console.WriteLine(detail);
            }

            //loop through and print specific details
            foreach (string detail in studentSpecificDetails) {
                Console.WriteLine(detail);
            }

            //loop through and print unique details
            foreach (string detail in studentUniqueDetails) {
                Console.WriteLine(detail);
            }

            Console.WriteLine("\n\nTesting details for staff: " + staff.ToString());
            Console.WriteLine("Image for this staff is: " + staffFromDB.photo);

            //loop through and print generic staff details
            foreach (string detail in staffGenericDetails) {
                Console.WriteLine(detail);
            }

            foreach (string detail in staffSpecificDetails) {
                Console.WriteLine(detail);
            }

            foreach (string detail in staffUniqueDetails) {
                Console.WriteLine(detail);
            }

            TestQ1IsCorrect(studentFromDB);
            TestTenureIsCorrect(studentFromDB);
            TestThreeYearAverageIsCorrect((Staff)staffFromDB);
            TestAndPrintStaffPerformance();
        }

        //Test Details of a researcher are correct
        public void TestQ1IsCorrect(Researcher r) {
            //(total # of Q1 publications/total # of publications * 100)
            //get total number of publications
            int totalPublications = r.PublicationsCount;
            Console.WriteLine("Total publications: " + totalPublications);

            //Count Q1 publications
            int Q1Publications = 0;
            foreach (Publication p in r.publications) {
                if (p.ranking == Ranking.Q1) {
                    Q1Publications++;
                }
            }
            Console.WriteLine("Total Q1 publications: " + Q1Publications);

            //Calculate Q1 percentage
            float Q1Percentage = (float)Q1Publications / totalPublications * 100;
            Console.WriteLine("Q1 calculated by " + Q1Publications +" / "+ totalPublications + " * 100 = " + Q1Percentage);
            Console.WriteLine("Calculated Q1: " + r.Q1percentage);

            if(Q1Percentage == r.Q1percentage) {
                Console.WriteLine("Q1 is correct");
            }
            else {
                Console.WriteLine("Q1 is incorrect");
            }
        }

        //Test Tenure is correct
        public void TestTenureIsCorrect(Researcher r) {
            //get tenure
            float tenure = r.Tenure;
            Console.WriteLine("Tenure: " + tenure);

            //get current date
            DateTime currentDate = DateTime.Now;

            //get difference between current date and start date
            TimeSpan tenureSpan = currentDate - r.utasStart;

            //Print tenureSpan
            Console.WriteLine("Tenure span Calculated by : " + currentDate.ToShortDateString() + "-" + r.utasStart.ToShortDateString() +" = "+ tenureSpan.Days + " Days " + tenureSpan.Hours+" Hours " + tenureSpan.Minutes+" Minutes ");

            //convert to years
            float tenureYears = (float)tenureSpan.TotalDays / 365;
            Console.WriteLine("Tenure calculated by " + tenureSpan.TotalDays + " / 365 = " + tenureYears);

            if (tenure == tenureYears) {
                Console.WriteLine("Tenure is correct");
            }
            else {
                Console.WriteLine("Tenure is incorrect");
            }
        }

        //Test 3 year average is correct
        //The total number of publications in the previous three whole calendar years,
        //divided by three. For example, if the current calendar year is 2019
        //then this is the average number of publications per year in the period
        //spanning 2016, 2017 and 2018. 
        public void TestThreeYearAverageIsCorrect(Staff r) {
            //get current date
            DateTime currentDate = DateTime.Now;

            //get difference between current date and start date
            TimeSpan tenureSpan = currentDate - r.utasStart;

            //convert to years
            float tenureYears = (float)tenureSpan.TotalDays / 365;

            //get oldest year
            int oldestYear = r.OldestPublicationYear();

            //get current year
            int currentYear = DateTime.Now.Year;

            //get 3 year average
            float threeYearAverage = r.ThreeYearAVG;

            //get total publications
            int totalPublications = r.PublicationsCount;

            //get total publications in last 3 years
            int totalPublicationsInLastThreeYears = 0;
            foreach (Publication p in r.publications) {
                if (p.yearPublished >= currentYear - 3) {
                    totalPublicationsInLastThreeYears++;
                }
            }

            //calculate 3 year average
            float calculatedThreeYearAverage = (float)totalPublicationsInLastThreeYears / 3;

            Console.WriteLine("3 year average calculated by " + totalPublicationsInLastThreeYears + " / 3 = " + calculatedThreeYearAverage);
            Console.WriteLine("3 year average: " + threeYearAverage);

            if (calculatedThreeYearAverage == threeYearAverage) {
                Console.WriteLine("3 year average is correct");
            }
            else {
                Console.WriteLine("3 year average is incorrect");
            }
        }

        //A helper function to setup a Staff with data required to calculate publication performance
        private Staff CreateStaffWithPublicationsAndTenure(int totalPublications, float tenureYears, EmploymentLevel level) {
            var staff = new Staff {
                level = level,
                utasStart = DateTime.Now.AddYears(-1 * (int)tenureYears), // Assuming tenure starts 'tenureYears' ago
                publications = new List<Publication>()
            };

            // Populate publications
            for (int i = 0; i < totalPublications; i++) {
                staff.publications.Add(new Publication {
                    yearPublished = DateTime.Now.Year - (i % 3), // Spread publications over the past three years
                                                                 // Set other necessary properties of Publication
                });
            }

            return staff;
        }

        //prints a comprehensive performance report for a staff member
        //for testing only.
        private void PrintPerformance(string description, Staff staff) {
            float expectedPublicationsPerYear = staff.ExpectedPublications;
            float threeYearAvg = staff.ThreeYearAVG;
            int publicationPerformance = staff.PublicationPerformance;

            Console.WriteLine($"{description}:");
            Console.WriteLine($" - Level: {staff.level}");
            Console.WriteLine($" - Tenure: {staff.Tenure} years");
            Console.WriteLine($" - Total Publications: {staff.PublicationsCount}");
            Console.WriteLine($" - Expected Publications per Year: {expectedPublicationsPerYear}");
            Console.WriteLine($" - Three-Year Average Publications: {threeYearAvg}");
            Console.WriteLine($" - Publication Performance (Publications/Tenure): {publicationPerformance.ToString()}");
        }

        public void TestAndPrintStaffPerformance() {
            // Create staff members for different performance metrics
            var poorPerformanceStaff = CreateStaffWithPublicationsAndTenure(3, 10, EmploymentLevel.RESEARCH_ASSOCIATE); // Low publications relative to tenure
            var belowExpectationsStaff = CreateStaffWithPublicationsAndTenure(15, 10, EmploymentLevel.LECTURER); // Slightly below expected ratio
            var meetingMinimumStaff = CreateStaffWithPublicationsAndTenure(20, 10, EmploymentLevel.ASSISTANT_PROFESSOR); // Meeting the expected ratio
            var starPerformerStaff = CreateStaffWithPublicationsAndTenure(45, 5, EmploymentLevel.PROFESSOR); // Exceeding the expected ratio significantly

            // Calculate and print performances for each staff member
            PrintPerformance("Poor Performance Staff", poorPerformanceStaff);
            PrintPerformance("Below Expectations Staff", belowExpectationsStaff);
            PrintPerformance("Meeting Minimum Staff", meetingMinimumStaff);
            PrintPerformance("Star Performer Staff", starPerformerStaff);
        }
    }
}
