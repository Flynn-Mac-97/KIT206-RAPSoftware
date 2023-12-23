using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    public class Publication
    {
        public string title { get; set; }
        public int yearPublished { get; set; }
        public string author { get; set; }
        public string DOI { get; set; }
        public Ranking ranking { get; set; }
        public PublicationType type { get; set; }
        public string citeLink { get; set; }
        public DateTime availability { get; set; }
        


        public Publication() 
        {
        }

        //returns the days elapsed since the publication became available; this can be negative if the availability date is in the future
        public int Age 
        { 
            get 
            {
                DateTime curr = DateTime.Today;
                int Days = (curr - availability).Days;

                return Days;
            } 
        }


        //an override for publication object to string;
        public override string ToString()
        {
            return (
                " Title: " + this.title +
                "\n Year Published: " + this.yearPublished +
                "\n Authors: " + this.author +
                "\n Date of Indentifier: " + this.DOI +
                "\n Ranking: " + this.ranking +
                "\n Publication Type: " + this.type +
                "\n Cite Link: " + this.citeLink +
                "\n Availability: " + this.availability.Date.ToString() +
                "\n Age: " + this.Age 
                );
        }

    }
}
