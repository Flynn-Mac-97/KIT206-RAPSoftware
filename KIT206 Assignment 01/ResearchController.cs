using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class ResearchController {
        public List<Researcher> researchers = new List<Researcher>();
        private EmploymentLevel employmentFilter;
        private string nameFilter;

        public void FetchDummyResearchers(int amount, GlobalDBAdaptor db) {
            this.researchers = db.GenerateDummyResearchers(amount);
        }

        //Print all researchers
        public void PrintAllResearchers()
        {
            //print out detail for each researcher.
            foreach (Researcher r in researchers)
            {
                Console.WriteLine(r.ToString());
            }
        }
    }
}