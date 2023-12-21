using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KIT206_Assignment_01
{
    /*public enum EmploymentLevel {
        RESEARCH_ASSOCIATE,  //level A
        LECTURER,            //level B
        ASSISTANT_PROFESSOR, //level C
        ASSOCIATE_PROFESSOR, //level D
        PROFESSOR,           //level E
        STUDENT
    };*/

    public class Position
    {
        public EmploymentLevel level { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }


        public override string ToString()
        {
            return $"{this.level}," + $"{this.startDate}," + $"{this.endDate}";
   
        }

        public string Title()
        {

            switch (level)
            {
                case EmploymentLevel.RESEARCH_ASSOCIATE:
                    return "Research Associate";
                case EmploymentLevel.LECTURER:
                    return "Lecturer";
                case EmploymentLevel.ASSISTANT_PROFESSOR:
                    return "Assistant Professor";
                case EmploymentLevel.ASSOCIATE_PROFESSOR:
                    return "Associate Professor";
                case EmploymentLevel.PROFESSOR:
                    return "Professor";
                default:
                    return "Student";

            }

        }

        public string ToTitle(EmploymentLevel l)
        {
            switch (l)
            {
                case EmploymentLevel.RESEARCH_ASSOCIATE:
                    return "Research Associate";
                case EmploymentLevel.LECTURER:
                    return "Lecturer";
                case EmploymentLevel.ASSISTANT_PROFESSOR:
                    return "Assistant Professor";
                case EmploymentLevel.ASSOCIATE_PROFESSOR:
                    return "Associate Professor";
                case EmploymentLevel.PROFESSOR:
                    return "Professor";
                default:
                    return "Student";

            }
        }


    }

}

