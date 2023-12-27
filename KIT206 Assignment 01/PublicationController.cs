using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    class PublicationController {

        // List of all publications for a researcher
        public List<Publication> publications = new List<Publication>();

        // List of filtered publications based on specific criteria
        public List<Publication> filteredPublications = new List<Publication>();



        //private employmentFilter;
        //private string yearFilter;

        private PublicationController()
        {

        }


        // fetches and stores a list of publications from the database
        public void FetchPublicationList(GlobalDBAdaptor db, int id)
        {
            this.publications = db.FetchPublicationListFromDB(id);
        }



        // filters publications by a selected range of publication year
        public void FilterByYearRange(int year1, int year2)
        {
            filteredPublications = publications
                .Where(p => (p.yearPublished >= year1 && p.yearPublished <= year2))
                .ToList();

        }

        // sorts publications in ascending order by publication year 
        public void SortByAscendingOrder()
        {
            publications.Sort((p1, p2) =>
            {
                int year = p1.yearPublished.CompareTo(p2.yearPublished);

                if (year == 0)
                {
                    return string.Compare(p1.DOI, p2.DOI, StringComparison.Ordinal);
                }
                else
                {
                    return year;
                }

            });

        }



    }
}
