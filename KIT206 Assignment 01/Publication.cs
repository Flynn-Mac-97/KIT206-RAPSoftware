using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Assignment_01
{
    public class Publication
    {
        public string title { get; set; }
        public  int yearPublished { get; set;}
        public List<Researcher> author { get; set; }
        public string DOI { get; set; }
        public int ranking { get; set; }
        public PublicationType type { get; set; }
        public string citeLink { get; set; }
        public Date availabilty { get; set; }
        public int age { get; set; }
        

    }
}
