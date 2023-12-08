using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class ResearchController {
        public List<Researcher> researchers = new List<Researcher>();
        public List<Researcher> filteredResearchers = new List<Researcher>();
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
        //Print all researchers
        public void PrintAllFilteredResearchers()
        {
            //print out detail for each researcher.
            foreach (Researcher r in filteredResearchers)
            {
                Console.WriteLine(r.ToString());
            }
        }

        //filter researchers based on a string and employment level
        public void FilterResearchers(string search, EmploymentLevel e)
        {
            filteredResearchers.Clear();
            foreach (Researcher r in researchers)
            {
                //if first or last name contains the search and job equals e, or search is nothing and job is e.
                if ((r.familyName.Contains(search) || r.givenName.Contains(search)) && r.currentPosition == e || search == "" && r.currentPosition == e)
                {
                    this.filteredResearchers.Add(r);
                }
            }
        }
    }
}