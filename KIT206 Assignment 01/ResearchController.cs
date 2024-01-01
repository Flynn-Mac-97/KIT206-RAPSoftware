using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace KIT206_Assignment_01 {
    // Defines the ResearchController class
    class ResearchController {
        private static ResearchController _instance;

        // List of filtered researchers based on specific criteria
        public ObservableCollection<ResearcherViewModel> FilteredResearcherNames { get; set; } = new ObservableCollection<ResearcherViewModel>();

        //List of names and data relating to the researcherList
        public ObservableCollection<ResearcherViewModel> ResearcherNames { get; set; }
        
        // The database adaptor
        private GlobalDBAdaptor db;

        // The currently selected researcher
        public Researcher SelectedResearcher { get; set; } = null;

        // List of staff for performance
        public ObservableCollection<Staff> StaffPerformanceList { get; set; } = new ObservableCollection<Staff>();

        //filtered staff performance list
        public ObservableCollection<Staff> FilteredStaffPerformanceList { get; set; } = new ObservableCollection<Staff>();

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
        public void FilterResearcher(string search, string level) {
            var filter = ResearcherNames.Where(r => r.FamilyName.Contains(search) || r.GivenName.Contains(search) || string.IsNullOrEmpty(search)).ToList();
            filter = filter.Where(r => level == "ALL" || r.Level.Equals(level, StringComparison.OrdinalIgnoreCase)).ToList();
            UpdateFilteredResearcherNames(filter);
        }

        // Filters researchers based solely on a name (either family or given name)
        public void FilterResearcherByName(string name) {
            FilteredResearcherNames.Clear();
            var filter = ResearcherNames
                .Where(r => r.FamilyName.Contains(name) || r.GivenName.Contains(name) || string.IsNullOrEmpty(name))
                .ToList();
            UpdateFilteredResearcherNames(filter);
        }

        // Filters researchers based solely on an employment level
        public void FilterResearcherByLevel(string level) {
            FilteredResearcherNames.Clear();
            var filter = ResearcherNames
                .Where(r => level == "ALL" || r.Level.Equals(level, StringComparison.OrdinalIgnoreCase))
                .ToList();
            UpdateFilteredResearcherNames(filter);
        }

        private void UpdateFilteredResearcherNames(List<ResearcherViewModel> filteredResults) {
            FilteredResearcherNames.Clear();
            foreach (var item in filteredResults) {
                FilteredResearcherNames.Add(item);
            }
        }

        // filters a list of researcher by their performance level
        public void FilterbyPerformance(ResearcherPerformance p)
        {
            //fetch the staff list if its empty
            if(StaffPerformanceList.Count <= 0) StaffPerformanceList = db.FetchStaffListForPerformance();

            //clear the filtered list
            FilteredStaffPerformanceList.Clear();

            foreach (Staff r in StaffPerformanceList)
            {
                //if they are staff then use their performance level.
                if (r.GetPerformanceLevel() == p) {
                    FilteredStaffPerformanceList.Add(r);
                }
            }

            //Depending on performance selection, sort the list
            if (p == ResearcherPerformance.POOR || p == ResearcherPerformance.BELOW_EXPECTATIONS) {
                //sort descending
                FilteredStaffPerformanceList = new ObservableCollection<Staff>(FilteredStaffPerformanceList.OrderByDescending(r => r.performance));
            }
            else {
                //sort ascending
                FilteredStaffPerformanceList = new ObservableCollection<Staff>(FilteredStaffPerformanceList.OrderBy(r => r.performance));
            }
        }

        // Fetches and returns an array of email addresses from a list of researchers
        public string[] FetchResearcherEmails() {
            //returns a string list of emails from FilteredStaffPerformanceList
            return FilteredStaffPerformanceList.Select(r => r.email).ToArray();
        }

        //Load a researcher
        public Researcher LoadResearcher(int id) {
            return db.FetchResearcherFromDB(id);
        }
    }
}
