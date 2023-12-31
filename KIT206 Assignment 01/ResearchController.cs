using System;
using System.Collections.Generic;
using System.Linq;

namespace KIT206_Assignment_01 {
    // Defines the ResearchController class
    class ResearchController {
        private static ResearchController _instance;
        // List of filtered researchers based on specific criteria
        public List<ResearcherViewModel> FilteredResearcherNames { get; set; }

        //List of names and data relating to the researcherList
        public List<ResearcherViewModel> ResearcherNames { get; set; }
        
        // The database adaptor
        private GlobalDBAdaptor db;

        // The currently selected researcher
        public Researcher SelectedResearcher { get; set; } = null;

        // Private constructor to prevent external instantiation
        private ResearchController() {
            db = new GlobalDBAdaptor();

            //fetch only the names of the researchers
            this.ResearcherNames = db.FetchResearcherViewModelListFromDB();
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

        //Clear the selected researcher
        public void ClearSelectedResearcher() {
            SelectedResearcher = null;
        }

        // Unused method intended to fetch a specified number of dummy researchers for testing
        public void FetchDummyResearchers(int amount, GlobalDBAdaptor db) {
            //this.researchers = db.GenerateDummyResearchers(amount);
        }

        // Prints all researchers' details to the console
        public void PrintAllResearchers() {
            ResearcherNames.ToList().ForEach(r => Console.WriteLine(r.ToString()));
        }

        // Prints all filtered researchers' details to the console
        public void PrintAllFilteredResearchers() {
            FilteredResearcherNames.ToList().ForEach(r => Console.WriteLine(r.ToString()));
        }

        // Filters researchers based on a search string and an employment level, then updates the filteredResearchers list
        public List<ResearcherViewModel> FilterResearcher(string search, string level) {

            return ResearcherNames
                .Where(r => (r.FamilyName.Contains(search) || r.GivenName.Contains(search) || search == ""))
                .ToList();
        }

        // Filters researchers based solely on a name (either family or given name)
        public void FilterResearcherByName(string name) {
            FilteredResearcherNames = ResearcherNames
                .Where(r => r.FamilyName.Contains(name) || r.GivenName.Contains(name))
                .ToList();
        }

        // filters a list of researcher by their performance level
        public List<Staff> FilterbyPerformance(ResearcherPerformance p)
        {
            Console.WriteLine("Filtering by performance: " + p);
            //Report rport = new Report();
            List<Staff> filteredStaff = new List<Staff>();

            /*foreach (Researcher r in researchers)
            {
                if(r is Student) { continue; } // skip students (they dont have performance levels

                //if they are staff then use their performance level.
                else if (r is Staff staff && staff.performance == p) {

                    if (p == ResearcherPerformance.POOR || p == ResearcherPerformance.BELOW_EXPECTATIONS)
                    {
        
                        filteredStaff.Add(staff);
                        filteredStaff.Sort((s1, s2) => s1.CalculatePerformance.CompareTo(s2.CalculatePerformance));
                    }

                    else
                    {
                        filteredStaff.Add(staff);
                        filteredStaff.Sort((s1, s2) => s2.CalculatePerformance.CompareTo(s1.CalculatePerformance));

                    }

                }
            }*/

            return filteredStaff;

        }

        /*// Fetches and returns an array of email addresses from a list of researchers
        public string[] FetchResearcherEmails(List<Staff> s)
        {
            // fillters only staff from researchers
            List<Researcher> filteredStaff = researchers.Where(researcher => s.Any(staff => staff == researcher)).ToList();
            return filteredStaff.Select(researcher => researcher.email).ToArray();
        }*/

        //Load a researcher
        public Researcher LoadResearcher(int id) {
            return db.FetchResearcherFromDB(id);
        }
    }
}
