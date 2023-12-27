using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Reflection;

namespace KIT206_Assignment_01 {
    public struct Project {
        public string Id { get; }
        public int Funding { get; }
        public int Year { get; }
        public List<int> ResearcherIds { get; }

        public Project(string id, int funding, int year, List<int> researcherIds) {
            Id = id;
            Funding = funding;
            Year = year;
            ResearcherIds = researcherIds;
        }
    }

    internal class GlobalXMLAdaptor {
        private static GlobalXMLAdaptor _instance;
        private static readonly object _lock = new object();
        private string _xmlFilePath;
        public List<Project> Projects { get; private set; }

        // Private constructor
        private GlobalXMLAdaptor(string xmlFilePath) {
            _xmlFilePath = xmlFilePath;
            Projects = LoadProjects();
        }

        // Public static method to get the instance
        public static GlobalXMLAdaptor GetInstance(string xmlFilePath) {
            lock (_lock) {
                if (_instance == null) {
                    _instance = new GlobalXMLAdaptor(xmlFilePath);
                }
                return _instance;
            }
        }

        // Load the list of projects from the XML file
        private List<Project> LoadProjects() {
            var xdoc = XDocument.Load(_xmlFilePath);
            var projects = xdoc.Descendants("Project")
                               .Select(p => new Project(
                                   p.Attribute("id").Value,
                                   int.Parse(p.Element("Funding").Value),
                                   int.Parse(p.Element("Year").Value),
                                   p.Element("Researchers").Elements("staff_id")
                                     .Select(r => int.Parse(r.Value)).ToList()
                               )).ToList();
            return projects;
        }

        // Return a list of projects that a researcher is involved in
        public List<Project> GetProjectsForResearcher(int researcherId) {
            return Projects.Where(p => p.ResearcherIds.Contains(researcherId)).ToList();
        }

        // Return total funding for a researcher from a list of projects
        public int GetFundingForResearcher(int researcherId) {
            return GetProjectsForResearcher(researcherId).Sum(p => p.Funding);
        }
    }
}
