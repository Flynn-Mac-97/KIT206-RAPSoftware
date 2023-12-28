using System;
using System.Collections.Generic;
using System.Linq;

namespace KIT206_Assignment_01 {
    // Defines the ResearchController class
    class ResearchController {
        private static ResearchController _instance;

        // List of all researchers
        public List<Researcher> researchers = new List<Researcher>();
        // List of filtered researchers based on specific criteria
        public List<Researcher> filteredResearchers = new List<Researcher>();
        //private EmploymentLevel employmentFilter;
        //private string nameFilter;

        private GlobalDBAdaptor db;

        // Private constructor to prevent external instantiation
        private ResearchController() {
            db = new GlobalDBAdaptor();
            //on initialisation, fetch the researcher data from DB
            FetchResearcherList(db);
        }

        // Public static property to access the instance
        public static ResearchController Instance {
            get {
                if (_instance == null) {
                    _instance = new ResearchController();
                }
                return _instance;
            }
        }

        // Unused method intended to fetch a specified number of dummy researchers for testing
        public void FetchDummyResearchers(int amount, GlobalDBAdaptor db) {
            //this.researchers = db.GenerateDummyResearchers(amount);
        }

        // Fetches and stores a list of researchers from the database
        public void FetchResearcherList(GlobalDBAdaptor db) {
            this.researchers = db.FetchCompleteListFromDB();
        }

        // Prints all researchers' details to the console
        public void PrintAllResearchers() {
            researchers.ToList().ForEach(r => Console.WriteLine(r.ToString()));
        }

        // Prints all filtered researchers' details to the console
        public void PrintAllFilteredResearchers() {
            filteredResearchers.ToList().ForEach(r => Console.WriteLine(r.ToString()));
        }

        // Filters researchers based on a search string and an employment level, then updates the filteredResearchers list
        public void FilterResearcher(string search, EmploymentLevel e) {
            filteredResearchers = researchers
                .Where(r => (r.familyName.Contains(search) || r.givenName.Contains(search) || search == ""))
                .ToList();
        }

        // Filters researchers based solely on a name (either family or given name)
        public void FilterResearcherByName(string name) {
            filteredResearchers = researchers
                .Where(r => r.familyName.Contains(name) || r.givenName.Contains(name))
                .ToList();
        }

        // filters a list of researcher by their performance level
        public List<Staff> FilterbyPerformance(ResearcherPerformance p)
        {
            Console.WriteLine("Filtering by performance: " + p);
            //Report rport = new Report();
            List<Staff> filteredStaff = new List<Staff>();

            foreach (Researcher r in researchers)
            {
                if(r is Student) { continue; } // skip students (they dont have performance levels

                //if they are staff then use their performance level.
                else if (r is Staff staff && staff.performance == p) {
                    filteredStaff.Add(staff);

                }
            }

            return filteredStaff;

        }

        // Fetches and returns an array of email addresses from a list of researchers
        public string[] FetchResearcherEmails(List<Staff> s)
        {
            // fillters only staff from researchers
            List<Researcher> filteredStaff = researchers.Where(researcher => s.Any(staff => staff == researcher)).ToList();
            return filteredStaff.Select(researcher => researcher.email).ToArray();
        }

    }
}
