using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class PublicationController {

        // List of all publications for a researcher
        public List<Publication> publications = new List<Publication>();

        // List of filtered publications based on specific criteria
        public List<Publication> filteredResearchers = new List<Publication>();
        //private employmentFilter;
        private string yearFilter;


        // fetches and stores a list of publications from the database
        public void FetchPublicationList(GlobalDBAdaptor db)
        {
            this.publications = db.FetchPublicationListFromDB();
        }


        // filters publications by publication year and availdability date
        public void FilterByYear(int year, int month)
        {

            filteredResearchers = publications
                .Where(p => (p.yearPublished == year || p.availability.Year == year)
                && (p.availability.Month == month)).ToList();

        }

        // sorts publications in ascending order by publication year 
        public void SortByAscendingOrder()
        {
            filteredResearchers.Sort((p1, p2) => p1.yearPublished.CompareTo(p2.yearPublished));

        }




        
        


    }
}
