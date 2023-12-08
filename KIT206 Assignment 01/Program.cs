
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

            researchController.FetchDummyResearchers(64, database);
            researchController.PrintAllResearchers();

            Console.WriteLine("Finished Generating Researchers");
        }
    }
}
