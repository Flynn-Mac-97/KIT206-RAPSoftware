using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class Report
    {
        public ResearcherPerformance performance { get; set; }
        public EmploymentLevel level { get; set; }
        private List<Researcher> researchers { get; set; }
        private ResearchController researchController;


        // calculates expected number of publications
        public float ExpectedPublications
        {
            get
            {
                float expectedPublications = 0;

                switch (level)
                {
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
        public float CalculatePerformance
        {
            get
            {
                Staff staff = new Staff();

                float performance = staff.ThreeYearAVG / ExpectedPublications * 100;

                return performance;
            }

        }


        // returns performance level based on the performance measure value
        public ResearcherPerformance PerformanceLevel()
        {

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

        

    }
    

}



     