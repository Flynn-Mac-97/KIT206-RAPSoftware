﻿using System;
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

    }
}
