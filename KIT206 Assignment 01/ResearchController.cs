using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

        //This is not needed anymore.
        public void SortPublicationList(Researcher[] researchers) {
            foreach (Researcher r in researchers) {
                r.publications.Sort(
                    delegate (Publication p1, Publication p2) {
                        int compareYear = p2.yearPublished.CompareTo(p1.yearPublished);
                        if (compareYear == 0) {
                            return p1.title.CompareTo(p2.title);
                        }
                        return compareYear;
                    }
                );
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
        // Updates the filteredResearchers list 
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

        //Get universal text data for staff and student to display on the researcher details page
        public string[] GenericResearcherDetails(Researcher selectedResearcher) {
            //Generic details
            return new string[]{ 
                selectedResearcher.givenName + " " + selectedResearcher.familyName,
                selectedResearcher.title.ToString().ToLower(),
                selectedResearcher.unit,
                selectedResearcher.campus.ToString().ToLower(),
                selectedResearcher.email,
                selectedResearcher.level.ToString().ToLower(),
            };
        }

        //Get more specific text data for staff and student to display on the researcher details page
        public string[] SpecificResearcherDetails(Researcher selectedResearcher) {
            return new string[] { 
                //Specific details
                "Commenced date: " + selectedResearcher.utasStart.ToShortDateString(),
                "Position Commenced: " + selectedResearcher.currentStart.ToShortDateString(),
                "Tenure: " + selectedResearcher.Tenure,
                "Total Publications: " + selectedResearcher.PublicationsCount,
                "Q1 Percentage: " + selectedResearcher.Q1percentage,
            };
        }

        //Return staff or student specific text data for staff and student to display on the researcher details page
        public string[] StaffOrStudentSpecificDetails(Researcher selectedResearcher) {
            if (selectedResearcher is Student student) {
                return new string[] {
                "Degree: " + student.degree,
                "Supervisor: " + student.supervisor.familyName + " " + student.supervisor.givenName, 
                };
            }
            else if (selectedResearcher is Staff staff) {
                Console.WriteLine(staff.ToString());
                staff.FundingRecieved = GlobalXMLAdaptor.GetInstance(Globals.XmlFilePath).GetFundingForResearcher(staff.id);
                return new string[] {
                    "3 year avg: " + staff.ThreeYearAVG,
                    "Funding Recieved: " + staff.FundingRecieved,
                    "Publication Performance: " + staff.PublicationPerformance,
                    "Funding Performance: " + staff.FundingPerformance,
                    "Staff Performance : " + staff.GetPerformanceLevel(),
                    (staff.SupervisionCount > 0 ) ? "Supervisions: " + staff.SupervisionCount + " " + staff.GetSupervisions() : "",
                };
            }
            else {
                return new string[] { };
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
