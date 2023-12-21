using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    public class Publication
    {
        public string title { get; set; }
        public int yearPublished { get; set; }
        public List<Researcher> author { get; set; }
        public string DOI { get; set; }
        public Ranking ranking { get; set; }
        public PublicationType type { get; set; }
        public string citeLink { get; set; }
        public DateTime availability { get; set; }
        


        public Publication() 
        {
        }

        //TO DO
        public int age { get; set; }




        


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
                "\n Age: " + this.age 
                );
        }

    }
}
