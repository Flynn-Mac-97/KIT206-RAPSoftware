using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class ResearchController
    {
        public List<Researcher> researchers = new List<Researcher>();
        public List<Researcher> filteredResearchers = new List<Researcher>();
        private EmploymentLevel employmentFilter;
        private string nameFilter;

        public void FetchDummyResearchers(int amount, GlobalDBAdaptor db)
        {
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

        //filter researchers based on name
        public void FilterResearcher(string search, EmploymentLevel e)
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

        //Filter Researcher by name only
        public void FilterResearcherByName(string name)
        {
            filteredResearchers.Clear();

            foreach (Researcher r in researchers)
            {
                if (r.familyName.Contains(name) || r.givenName.Contains(name))
                {
                    researchers.Add(r);
                }
            }
        }
        //Filter researcher by Employment
        public void FilterResearcherByEmployment(EmploymentLevel e)
        {
            filteredResearchers.Clear();

            foreach (Researcher r in researchers)
            {
                if (r.currentPosition == e)
                {
                    researchers.Add(r);
                }
            }
        }
        //Load staff specific details
        public void LoadStaffDetails()
        {

        }
        //Load student specific details
        public void LoadStudentDetails()
        {

        }
        //Given list of researchers, get their emails.
        public string[] FetchResearcherEmails(List<Researcher> r)
        {
            string[] emails = new string[r.Count];
            int i = 0;
            foreach (Researcher researcher in r)
            {
                emails[i] = researcher.email;
                i++;
            }
            return emails;
        }
    }
}