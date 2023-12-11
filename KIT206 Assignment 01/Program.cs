
//hello this is midori:)
using System;

namespace KIT206_Assignment_01
{
    class Program
    {
        static void Main(string[] args)
        {
            ResearchController researchController = new ResearchController();
            GlobalDBAdaptor database = new GlobalDBAdaptor();

            researchController.FetchDummyResearchers(16, database);

            Console.WriteLine("All Researchers:\n");
            researchController.PrintAllResearchers();

            Console.WriteLine("Filtered Researchers:\n");
            researchController.FilterResearcher("", EmploymentLevel.LECTURER);
            researchController.PrintAllFilteredResearchers();

            Console.WriteLine("Finished Generating Researchers");
        }
    }
}
