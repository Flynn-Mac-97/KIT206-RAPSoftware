using System;
using System.Collections.Generic;

namespace KIT206_Assignment_01
{
    class Staff : Researcher
    {
        public float threeYearAVG { get; set; }
        public int fundingRecieved { get; set; }
        public float publicationPerformance { get; set; }
        public List<Student> supervisions { get; set; }
    }
}
