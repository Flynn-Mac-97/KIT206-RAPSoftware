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

        //returns the expected number of publications per years for corresponding level of employments.
        public float ExpectedPublications(EmploymentLevel e)
        {
            float expected = 0;

            switch (e)
            {
                case EmploymentLevel.RESEARCH_ASSOCIATE:
                    expected = 0.5f; break;
                case EmploymentLevel.LECTURER:
                    expected = 1; break;
                case EmploymentLevel.ASSISTANT_PROFESSOR:
                    expected = 2; break;
                case EmploymentLevel.ASSOCIATE_PROFESSOR:
                    expected = 3; break;
                case EmploymentLevel.PROFESSOR:
                    expected = 3.2f; break;
            }

            return expected;
        }

    }
}
